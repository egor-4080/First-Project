using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float maxHealthPoints;
    [SerializeField] protected float speed;
    [SerializeField] protected AudioSource takeDamageSound;

    protected SpriteRenderer spriteRenderer;
    protected float currentHealthPoints;
    protected int spritesLength;
    protected bool isAlive = true;
    protected bool isStatic = true;

    protected void Start()
    {
        Invoke("StartAnimation", 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
        {
            currentHealthPoints -= takenDamage;
            if (currentHealthPoints <= 0)
            {
                Destroy(gameObject);
                //Destroy(gameObject, spritesLength / 8f / 5f / 2f * spritesLength);
                isAlive = false;
            }
        }
    }
}