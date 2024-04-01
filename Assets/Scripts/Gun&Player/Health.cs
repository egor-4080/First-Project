using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private UnityEvent onDamge;
    [SerializeField] private UnityEvent onDeath;

    private AudioSource takeDamageSound;
    private float currentHealthPoints;
    private bool isAlive = true;

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
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
                Destroy(gameObject);
                isAlive = false;
            }
        }
    }
}
