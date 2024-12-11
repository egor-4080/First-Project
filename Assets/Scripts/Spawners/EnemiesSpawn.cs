using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private TimerController timer;
    [SerializeField] private float spawnTime;
    [SerializeField] private int countEnemy;

    [SerializeField] private string dictionaryName;
    [SerializeField] private string[] staticNames;

    [SerializeField] private float speedInc;
    [SerializeField] private float damageInc;
    [SerializeField] private float healthInc;

    private GameObject currentEnemy;

    private int currentEnemies;
    private bool isTimerActive;

    public UnityEvent newxtWave { get; private set; } = new();

    private void Start()
    {
        timer.timerEnd.AddListener(() => isTimerActive = false);
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
        StartCoroutine(StartWaves());
    }

    private IEnumerator StartWaves()
    {
        var dictionary = Config.instance.configStats[dictionaryName];
        while (true)
        {
            currentEnemies = countEnemy;
            dictionary["damage"] += damageInc;
            dictionary["speedForce"] += speedInc;
            dictionary["maxHealthPoints"] += healthInc;
            for (int i = 0; i < countEnemy; i++)
            {
                currentEnemy = enemy[Random.Range(0, enemy.Length)];

                GameObject enemyObject = PhotonNetwork.InstantiateRoomObject(currentEnemy.name
                    , spawnPoints[Random.Range(0, spawnPoints.Length)].position
                    , currentEnemy.transform.rotation);
                Health enemyHealth = enemyObject.GetComponent<Health>();
                enemyHealth.SetMaxHealth(dictionaryName);

                enemyHealth.OnDeath.AddListener(() => currentEnemies--);

                yield return new WaitForSeconds(spawnTime);
            }
            countEnemy += 2;
            while(currentEnemies != 0)
            {
                yield return null;
            }
            timer.StartTimer(10);
            isTimerActive = true;
            while(isTimerActive)
            {
                yield return null;
            }
        }
    }
}