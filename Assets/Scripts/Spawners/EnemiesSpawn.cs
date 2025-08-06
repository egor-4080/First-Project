using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using YG;
using YG.Utils.LB;
using Random = UnityEngine.Random;

public class EnemiesSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private TimerController timer;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private GodMode godMode;
    [SerializeField] private float spawnTime;
    [SerializeField] private int countEnemy;

    [SerializeField] private string dictionaryName;
    [SerializeField] private string[] staticNames;

    [SerializeField] private float speedInc;
    [SerializeField] private float damageInc;
    [SerializeField] private float healthInc;

    private int currentEnemies;

    private GameObject currentEnemy;
    private PhotonView photon;
    private bool isTimerActive;

    public UnityEvent newxtWave { get; private set; } = new();

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        timer.timerEnd.AddListener(() => isTimerActive = false);
        YG2.onGetLeaderboard += OnGetLeaderBoard;
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

    private void OnGodMode(EnemyAttack enemyAttack)
    {
        if (godMode.setter)
        {
            enemyAttack.ChangeDamage(0);
        }
    }

    private void OnGetLeaderBoard(LBData data)
    {
        Player player = PhotonNetwork.LocalPlayer;
        int currentScore = data.currentPlayer.score + (int)player.CustomProperties["Score"];
        YG2.SetLeaderboard("LeaderBoard", currentScore);
    }

    [PunRPC]
    private void GetLeaderBoard()
    {
        YG2.GetLeaderboard("LeaderBoard");
    }

    private IEnumerator StartWaves()
    {
        var dictionary = Config.instance.configStats[dictionaryName];
        dictionary["damage"] = 0;
        dictionary["speedForce"] = 0;
        dictionary["maxHealthPoints"] = 0;
        
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
                OnGodMode(enemyObject.GetComponent<EnemyAttack>());
                
                enemyHealth.OnDeath.AddListener(() => currentEnemies--);

                yield return new WaitForSeconds(spawnTime);
            }

            countEnemy += 2;
            while (currentEnemies != 0)
            {
                yield return null;
            }

            timer.StartTimer(10);
            isTimerActive = true;
            while (isTimerActive)
            {
                yield return null;
            }
            
            photon.RPC(nameof(GetLeaderBoard), RpcTarget.All);
        }
    }
}