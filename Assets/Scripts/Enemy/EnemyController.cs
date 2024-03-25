using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;

public class EnemyController : Character
{
    private Transform player;
    private NavMeshAgent agent;
    private List<Transform> players;

    private float startScaleX;
    private float startScaleY;
    private int setScale;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerContoller>().transform;
    }

    protected override void Start()
    {
        base.Start();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;
    }

    private void Update()
    {
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

    void FindNearestPlayer()
    {
        this.players = PlayerSpawner.GetPlayers();
    }
}