using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnTime;

    private WaitForSeconds wait;
    private GameObject currentEnemy;

    private void Start()
    {
        FindMasterToSpawn();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        FindMasterToSpawn();
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
        currentEnemy = enemy[Random.Range(0, enemy.Length)];
        PhotonNetwork.InstantiateRoomObject(currentEnemy.name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, currentEnemy.transform.rotation);
        yield return wait;
        StartCoroutine(WaitForSpawmEnemy());
    }
}
