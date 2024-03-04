using System.Collections;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnTime;

    private WaitForSeconds wait;

    private void Start()
    {
        StartCoroutine(WaitForSpawmEnemy());
        wait = new WaitForSeconds(spawnTime);
    }

    private IEnumerator WaitForSpawmEnemy()
    {
        while (true)
        {
            Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            yield return wait;
        }
    }
}
