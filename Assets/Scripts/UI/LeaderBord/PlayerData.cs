using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public UnityEvent Scored { get; private set; } = new UnityEvent();
    public string Name { get; private set; }
    public int Score { get; private set; }

    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textScore;
    
    private LeaderDataController dataController;
    private Player player;
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
        player = PhotonNetwork.LocalPlayer;
    }

    private void Start()
    {
        Scored.AddListener(OnScore);
        SetPosition();
    }

    public void Init(string name)
    {
        Name = name;
        Score = 0;
        
        photon.RPC(nameof(NetworkChange), RpcTarget.AllBuffered, Name, Score);
    }

    [PunRPC]
    private void NetworkChange(string name, int score)
    {
        textName.text = name;
        textScore.text = score.ToString();
    }
    
    private void SetPosition()
    {
        Transform canvasPosition = GameObject.FindWithTag("LeadersData").GetComponent<Transform>();
        transform.SetParent(canvasPosition);
        transform.localScale = new Vector3(1, 1, 1);
        dataController = GetComponentInParent<LeaderDataController>();
    }
    
    private void OnScore()
    {
        print(player.CustomProperties["Score"]);
        Score = (int)player.CustomProperties["Score"];
        photon.RPC(nameof(NetworkChange), RpcTarget.All, Name, Score);
        dataController.ChangeBoard();
    }
}
