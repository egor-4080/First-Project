using Photon.Pun;
using UnityEngine;

public abstract class Character : MonoBehaviourPunCallbacks
{
    [SerializeField] protected float speedForce;

    protected Rigidbody2D rigitBody;

    //animations integers
    protected int TurnByX;
    protected int TurnByY;

    protected virtual void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
    }

    public virtual void OnDeath()
    {
    }

    public virtual void OnHit()
    {
    }
}