using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Range(0, 100)] protected float maxHealthPoints;
    [SerializeField] protected float speed;
    
    protected AudioSource takeDamageSound;
    protected float currentHealthPoints;
    protected int spritesLength;
    protected bool isAlive = true;

    //animations integers
    protected int TurnByX;
    protected int TurnByY;

    protected virtual void Start()
    {
        takeDamageSound = GetComponent<AudioSource>();
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
        {
            takeDamageSound.Play();
            currentHealthPoints -= takenDamage;
            if (currentHealthPoints <= 0)
            {
                Destroy(gameObject);
                isAlive = false;
            }
        }
    }
}