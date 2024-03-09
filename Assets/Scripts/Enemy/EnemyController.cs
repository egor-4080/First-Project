using UnityEngine.AI;
using UnityEngine;

public class EnemyController : Character
{
    private Transform player;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerContoller>().transform;
    }

    protected override void Start()
    {
        base.Start();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}