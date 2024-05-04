using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints;
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
        currentHealthPoints = maxHealthPoints;
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
        }
    }

    public void HealCharacter(float heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    private void DestroyForAnim()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(photon);
        }
    }
}
