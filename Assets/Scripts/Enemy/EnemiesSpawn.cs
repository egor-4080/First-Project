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
        wait = new WaitForSeconds(spawnTime);
        StartCoroutine(WaitForSpawmEnemy());
    }

    public IEnumerator WaitForSpawmEnemy()
    {
        PhotonNetwork.Instantiate(enemy[Random.Range(0, enemy.Length)].name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.Euler(0, 0, 0));
        yield return wait;
        StartCoroutine(WaitForSpawmEnemy());
    }
}
