using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private UnityEvent onDamge;
    [SerializeField] private UnityEvent onDeath;

    private AudioSource takeDamageSound;
    private PhotonView photon;
    private float currentHealthPoints;
    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealthPoints;
            healthSlider.value = healthSlider.maxValue;
        }
        currentHealthPoints = maxHealthPoints;
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
        if (IsAlive)
        {
            onDamge.Invoke();
            currentHealthPoints -= takenDamage;
            if (takeDamageSound != null)
            {
                takeDamageSound.Play();
            }
            if (currentHealthPoints <= 0)
            {
                onDeath.Invoke();
                IsAlive = false;
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
