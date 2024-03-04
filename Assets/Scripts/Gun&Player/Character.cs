using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float maxHealthPoints;
    [SerializeField] protected float speed;
    [SerializeField] protected Sprite[] damagadedSprites;
    [SerializeField] protected Sprite[] staticSprites;
    [SerializeField] protected Sprite[] deadSprites;
    [SerializeField] protected Sprite mainSprite;
    [SerializeField] protected AudioSource takeDamageSound;

    protected SpriteRenderer spriteRenderer;
    protected float currentHealthPoints;
    protected bool isAlive = true;
    protected int spritesLength;

    protected void Start()
    {
        Invoke("StartAnimation", 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealthPoints = maxHealthPoints;
        StartCoroutine(GetSpriteAnimation(staticSprites, 1f));
    }

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
        {
            //takeDamageSound.Play();
            StopAllCoroutines();
            spriteRenderer.sprite = mainSprite;
            StartCoroutine(GetSpriteAnimation(damagadedSprites, 1f));

            currentHealthPoints -= takenDamage;
            if (currentHealthPoints <= 0)
            {
                spritesLength = deadSprites.Length;
                Destroy(gameObject, spritesLength / 8f / 5f / 2f * spritesLength);
                StopAllCoroutines();
                StartCoroutine(GetSpriteAnimation(deadSprites, 2f));
                isAlive = false;
            }
        }
    }

    protected IEnumerator GetSpriteAnimation(Sprite[] sprites, float acceleration)
    {
        spritesLength = sprites.Length;
        var animationTime = spritesLength / 8f / 5f / acceleration;
        for (int i = 0; i < spritesLength; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(animationTime);
        }
        spriteRenderer.sprite = mainSprite;
        StartCoroutine(GetSpriteAnimation(staticSprites, 1f));
    }
}