using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    [SerializeField] private GameObject hpBar;
    private NavMeshAgent agent;
    private Animator animator;

    private Transform player;
    private List<Transform> players;
    private int setScale;

    private float startScaleX;
    private float startScaleY;

    protected override void Awake()
    {
        base.Awake();

        InitSettings();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        FindMasterToStartMoving();
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
            hpBar.transform.localScale = new Vector3(setScale * 1, 1, 1);
            agent.SetDestination(player.position);
        }
    }

    public void InitSettings()
    {
        var dictionary = Config.instance.configStats["EnemyDictionary"];
        speedForce += dictionary["speedForce"];
        damage += dictionary["damage"];
    }

    public void FindMasterToStartMoving()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            enabled = false;
            return;
        }
        else
        {
            enabled = true;
        }

        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;
    }

    public override void OnDeath()
    {
        animator.SetBool("isDeath", true);
        agent.SetDestination(transform.position);
        enabled = false;
        Destroy(this);
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
        foreach (Transform player in players)
        {
            if (player != null)
            {
                float currentLength = (player.position - transform.position).magnitude;
                if (minimum > currentLength)
                {
                    minimum = currentLength;
                    nearestPlayer = player;
                }
            }
        }

        this.player = nearestPlayer;
    }
}