using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Range(0, 5000)] protected float maxHealthPoints;
    [SerializeField] protected float speedForce;
    [SerializeField] protected float damage;

    private AudioSource takeDamageSound;
    protected float currentHealthPoints;
    protected Rigidbody2D rigitBody;
    private bool isAlive = true;

    //animations integers
    protected int TurnByX;
    protected int TurnByY;

    protected virtual void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
        takeDamageSound = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerContoller player))
        {
            player.TakeDamage(damage);
        }
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

    public void HealCharacter(float heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    public void FreezCharacter(float unFreezWait)
    {
        if (TryGetComponent(out NavMeshAgent enemy))
        {
            unFreezWait = enemy.speed / 5;
        }
        else
        {
            unFreezWait = speedForce / 5;
        }
        speedForce = 0;
        StartCoroutine(FreezMotionCharacter(unFreezWait));
    }

    private IEnumerator FreezMotionCharacter(float unFreezWait)
    {
        for (var i = 0f; i < speedForce; i += unFreezWait)
        {
            speedForce = i;
            yield return unFreezWait;
        }
    }
}