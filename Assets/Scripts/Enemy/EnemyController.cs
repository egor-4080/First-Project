using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.Linq;

public class EnemyController : Character
{
    private Transform player;
    private NavMeshAgent agent;
    private List<Transform> players;
    private Animator animator;

    private float startScaleX;
    private float startScaleY;
    private int setScale;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        print(PhotonNetwork.IsMasterClient);
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
            return;
        }
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;
    }

    private void Update()
    {
        FindNearestPlayer();
        if (player != null)
        {
            if (transform.position.x - player.position.x > 0)
            {
                setScale = -1;
            }
            else
            {
                setScale = 1;
            }
            transform.localScale = new Vector3(setScale * startScaleX, startScaleY, 1);
            agent.SetDestination(player.position);
        }
    }

    public override void OnDeath()
    {
        animator.SetBool("isDeath", true);
        agent.SetDestination(transform.position);
        enabled = false;
    }

    public override void OnHit()
    {
        animator.SetTrigger("Hit");
    }

    void FindNearestPlayer()
    {
        float minimum = float.MaxValue;
        Transform nearestPlayer = null;
        players = PlayerSpawner.players;
        foreach(Transform player in players)
        {
            float currentLength = (player.position - transform.position).magnitude;
            if(minimum > currentLength)
            {
                minimum = currentLength;
                nearestPlayer = player;
            }
        }
        this.player = nearestPlayer;
    }
}