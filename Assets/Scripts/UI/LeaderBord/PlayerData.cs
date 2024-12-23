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
        dataController = GetComponentInParent<LeaderDataController>();
        photon = GetComponent<PhotonView>();
        player = PhotonNetwork.LocalPlayer;
    }

    private void Start()
    {
        Init();
        Scored.AddListener(OnScore);
        SetPosition();
    }

    private void SetPosition()
    {
        Transform canvasPosition = GameObject.FindWithTag("LeadersData").GetComponent<Transform>();
        transform.SetParent(canvasPosition);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Init()
    {
        Name = PhotonNetwork.LocalPlayer.NickName;
        Score = 0;

        textName.text = Name;
        textScore.text = Score.ToString();
    }
    
    private void OnScore()
    {
        Score = (int)player.CustomProperties["Score"];
        dataController.ChangeBoard();
    }
}
