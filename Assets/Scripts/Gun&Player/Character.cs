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

    //animations booleans
    protected int TurnByX;
    protected int TurnByY;

    protected void Start()
    {
        takeDamageSound = GetComponent<AudioSource>();
        currentHealthPoints = maxHealthPoints;
        DoAnotherStart();
    }
    abstract public void DoAnotherStart();

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