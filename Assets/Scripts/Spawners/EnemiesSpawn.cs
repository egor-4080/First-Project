using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnTime;
    [SerializeField] private int countEnemy;

    [SerializeField] private float speedInc;
    [SerializeField] private float damageInc;
    [SerializeField] private float healthInc;
    [SerializeField] private int countEnemyInc;

    private WaitForSeconds wait;
    private GameObject currentEnemy;

    private void Start()
    {
        FindMasterToSpawn();
        if (Config.instance.config.ContainsKey("damage") == false)
        {
            Config.instance.config.Add("damage", 0);
        }
        if (Config.instance.config.ContainsKey("speedForce") == false)
        {
            Config.instance.config.Add("speedForce", 0);
        }
        if (Config.instance.config.ContainsKey("maxHealthPoints") == false)
        {
            Config.instance.config.Add("maxHealthPoints", 0);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        FindMasterToSpawn();
    }

    public void SpawnEnemy()
    {

    }

    private void FindMasterToSpawn()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        wait = new WaitForSeconds(spawnTime);
        StartCoroutine(WaitForSpawmEnemy());
    }

    private IEnumerator WaitForSpawmEnemy()
    {
        while (true)
        {
            Config.instance.config["damage"] += damageInc;
            Config.instance.config["speedForce"] += speedInc;
            Config.instance.config["maxHealthPoints"] += healthInc;
            for (int i = 0; i < countEnemy; i++)
            {
                currentEnemy = enemy[Random.Range(0, enemy.Length)];
                PhotonNetwork.InstantiateRoomObject(currentEnemy.name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, currentEnemy.transform.rotation);
                yield return wait;
            }
        }
    }
}
