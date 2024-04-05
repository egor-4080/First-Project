using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speedForce;
    [SerializeField] protected float damage;

    private AudioSource takeDamageSound;
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(damage);
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

    public virtual void OnDeath()
    {

    }
}