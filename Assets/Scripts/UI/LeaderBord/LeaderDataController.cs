using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LeaderDataController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefab;

    private List<PlayerData> playerDatas;
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        MakeData();
        Invoke(nameof(GetDatas), 0.1f);
    }

    public void ChangeBoard()
    {
        photon.RPC(nameof(NetworkChange), RpcTarget.All);
    }
    
    private void NetworkChange()
    {
        //Реализовать пузырьковой сортировкой
    }

    private void GetDatas()
    {
        playerDatas = new List<PlayerData>();
        foreach(Transform child in transform)
        {
            playerDatas.Add(child.GetComponent<PlayerData>());
        }
    }

    private void MakeData()
    {
        PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
    }
}
