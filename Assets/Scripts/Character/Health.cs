using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private int scorePrice;
    [SerializeField] private int coinPrice;

    private float currentHealthPoints;
    private bool isHuman;

    private PlayerLifesController lifesController;
    private Collider2D objectCollider;
    private PhotonView photon;
    private Player player;
    private AudioSource takeDamageSound;

    public UnityEvent OnDeath => onDeath;
    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
        objectCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealthPoints;
            healthSlider.value = healthSlider.maxValue;
        }

        currentHealthPoints = maxHealthPoints;
        if (GetComponent<PlayerContoller>() != null)
        {
            lifesController = FindFirstObjectByType<PlayerLifesController>();
            if (lifesController != null)
            {
                lifesController.Init(this);
            }
        }
    }

    public void SetMaxHealth(string key)
    {
        var dictionary = Config.instance.configStats[key];
        maxHealthPoints += dictionary["maxHealthPoints"];
    }

    public void IsHuman(bool isHuman)
    {
        this.isHuman = isHuman;
    }

    private void UpdateHPBar()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = currentHealthPoints;
        }
    }

    public void TakeDamage(float takenDamage)
    {
        if (IsAlive && isHuman && currentHealthPoints - takenDamage <= 0)
        {
            ChangeMoney();
            ChangeScore();
        }

        photon.RPC(nameof(NetworkDamage), RpcTarget.All, takenDamage);
    }

    private void ChangeScore()
    {
        player = PhotonNetwork.LocalPlayer;
        Hashtable playerProperties = player.CustomProperties;
        int score = (int)playerProperties["Score"] + scorePrice;
        playerProperties["Score"] = score;
        player.SetCustomProperties(playerProperties);
    }

    private void ChangeMoney()
    {
        CoinManager.Instanse.AddMoney(coinPrice);
    }

    [PunRPC]
    public void NetworkDamage(float takenDamage)
    {
        if (IsAlive)
        {
            onDamage.Invoke();
            currentHealthPoints -= takenDamage;
            if (takeDamageSound != null)
            {
                takeDamageSound.Play();
            }

            if (currentHealthPoints <= 0)
            {
                onDeath.Invoke();
                healthSlider.gameObject.SetActive(false);
                GetComponent<Collider2D>().isTrigger = true;
                IsAlive = false;
                objectCollider.isTrigger = true;
            }
            else
            {
                UpdateHPBar();
            }
        }
    }

    public void HealCharacter(float heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }

        UpdateHPBar();
    }

    private void DestroyForAnim()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(photon);
        }
    }
}