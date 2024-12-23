using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private int price;

    public UnityEvent OnDeath => onDeath;

    private Rigidbody2D rigitBody;
    private Collider2D objectCollider;
    private AudioSource takeDamageSound;
    private PhotonView photon;
    private Player player;
    private float currentHealthPoints;
    private bool isHuman;
    private LeaderDataController leaderBoard;
    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Base")
        {
            leaderBoard = GameObject.FindWithTag("LeadersData").GetComponent<LeaderDataController>();
        }
        photon = GetComponent<PhotonView>();
        objectCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if(TryGetComponent(out Rigidbody2D rigitBody))
        {
            this.rigitBody = rigitBody;
        }
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealthPoints;
            healthSlider.value = healthSlider.maxValue;
        }
        currentHealthPoints = maxHealthPoints;
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
        if(healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = currentHealthPoints;
        }
    }
    
    public void TakeDamage(float takenDamage)
    {
        if (IsAlive && isHuman && currentHealthPoints - takenDamage <= 0)
        {
            player = PhotonNetwork.LocalPlayer;
            Hashtable playerProperties = player.CustomProperties;
            int score = (int)playerProperties["Score"] + price;
            playerProperties["Score"] = score;
            player.SetCustomProperties(playerProperties);
            PlayerData data = leaderBoard.GetPlayerData(player.NickName);
            data.Scored.Invoke();
        }
        photon.RPC(nameof(NetworkDamage), RpcTarget.All, takenDamage);
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
                IsAlive = false;
                objectCollider.isTrigger = true;
            }
            UpdateHPBar();
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
