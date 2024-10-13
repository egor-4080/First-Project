using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviourPunCallbacks
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

    public virtual void OnHit()
    {

    }
}