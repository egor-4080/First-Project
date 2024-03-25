using System.Collections;
using Photon.Pun;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnTime;

    private WaitForSeconds wait;

    private void Start()
    {
        StartCoroutine(WaitForSpawmEnemy());
        wait = new WaitForSeconds(spawnTime);
    }

    private IEnumerator WaitForSpawmEnemy()
    {
        PhotonNetwork.Instantiate(enemy[Random.Range(0, enemy.Length)].name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        yield return wait;
        StartCoroutine(WaitForSpawmEnemy());
    }
}
