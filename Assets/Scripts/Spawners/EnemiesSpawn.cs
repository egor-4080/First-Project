using System.Collections;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnTime;
    [SerializeField] private int countEnemy;

    [SerializeField] private string dictionaryName;
    [SerializeField] private string[] staticNames;

    [SerializeField] private float speedInc;
    [SerializeField] private float damageInc;
    [SerializeField] private float healthInc;

    private GameObject currentEnemy;

    private void Start()
    {
        Config.instance.SetNewDictionary(dictionaryName, staticNames);
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
        StartCoroutine(WaitForSpawmEnemy());
    }

    private IEnumerator WaitForSpawmEnemy()
    {
        var dictionary = Config.instance.config[dictionaryName];
        while (true)
        {
            countEnemy += 5;
            dictionary["damage"] += damageInc;
            dictionary["speedForce"] += speedInc;
            dictionary["maxHealthPoints"] += healthInc;
            for (int i = 0; i < countEnemy; i++)
            {
                currentEnemy = enemy[Random.Range(0, enemy.Length)];

                PhotonNetwork.InstantiateRoomObject(currentEnemy.name
                    , spawnPoints[Random.Range(0, spawnPoints.Length)].position
                    , currentEnemy.transform.rotation)
                    .GetComponent<Health>().SetMaxHealth(dictionaryName);

                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}
