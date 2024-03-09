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
    protected bool isStatic = true;
    protected bool isTurnByX = true;
    protected bool isturnByY = true;

    private void Start()
    {
        takeDamageSound = GetComponent<AudioSource>();
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
        {
            //takeDamageSound.Play();
            currentHealthPoints -= takenDamage;
            if (currentHealthPoints == 0)
            {
                Destroy(gameObject);
                //Destroy(gameObject, spritesLength / 8f / 5f / 2f * spritesLength);
                isAlive = false;
            }
        }
    }
}