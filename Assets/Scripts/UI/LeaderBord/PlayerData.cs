using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textScore;
    
    private LeaderDataController dataController;
    private Transform canvasPosition;
    private Player player;
    private PhotonView photon;

    private float counterPosition;

    private void Awake()
    {
        canvasPosition = GameObject.FindWithTag("LeadersData").GetComponent<Transform>();
        photon = GetComponent<PhotonView>();
        player = PhotonNetwork.LocalPlayer;
        transform.SetParent(canvasPosition);
        transform.localScale = Vector3.one;
    }

    public void Init(string name, LeaderDataController dataController, int counterPosition)
    {
        Name = name;
        Score = 0;
        this.dataController = dataController;
        this.counterPosition = counterPosition;
        
        photon.RPC(nameof(NetworkChange), RpcTarget.AllBuffered, Name, Score);
    }

    [PunRPC]
    private void NetworkChange(string name, int score)
    {
        textName.text = name;
        textScore.text = score.ToString();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Score") && photon.IsMine && dataController)
        {
            Score = (int)player.CustomProperties["Score"];
            photon.RPC(nameof(NetworkChange), RpcTarget.All, Name, Score);
            dataController.ChangeBoard();
        }
    }
}