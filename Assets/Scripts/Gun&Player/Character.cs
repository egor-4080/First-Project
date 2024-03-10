using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Range(0, 5000)] protected float maxHealthPoints;
    [SerializeField] protected float speedForce;

    private AudioSource takeDamageSound;
    private float currentHealthPoints;
    private WaitForSeconds wait;
    private bool isAlive = true;
    protected float kill = 1;

    //animations integers
    protected int TurnByX;
    protected int TurnByY;

    protected virtual void Start()
    {
        takeDamageSound = GetComponent<AudioSource>();
        currentHealthPoints = maxHealthPoints;
        wait = new WaitForSeconds(1);
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

    private IEnumerator Killing()
    {
        kill = 2;
        yield return wait;
        kill = 1;
    }
}