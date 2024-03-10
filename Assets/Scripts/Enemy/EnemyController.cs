using UnityEngine.AI;
using UnityEngine;

public class EnemyController : Character
{
    private Transform player;
    private NavMeshAgent agent;

    private float startScaleX;
    private float startScaleY;
    private int setScale;

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
        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;
    }

    private void Update()
    {
        if(transform.position.x - player.position.x > 0)
        {
            setScale = -1;
        }
        else
        {
            setScale = 1;
        }
        transform.localScale = new Vector3(setScale * startScaleX, startScaleY, 1);

        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}