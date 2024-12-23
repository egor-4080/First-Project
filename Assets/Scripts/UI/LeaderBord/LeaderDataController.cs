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

    public PlayerData GetPlayerData(string name)
    {
        foreach (var data in playerDatas)
        {
            if (data.Name == name)
            {
                return data;
            }
        }

        return null;
    }

    public void ChangeBoard()
    {
        SortDatas();
        photon.RPC(nameof(NetworkChange), RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GetDatas();
    }

    private void SortDatas()
    {
        PlayerData temp;
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].Score > playerDatas[j + 1].Score)
                {
                    temp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = temp;
                }
            }
        }
        playerDatas.Reverse();
    }

    [PunRPC]
    private void NetworkChange()
    {
        foreach (var data in playerDatas)
        {
            data.transform.SetParent(null);
        }

        foreach (var data in playerDatas)
        {
            data.transform.SetParent(transform);
        }
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
        PlayerData data = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<PlayerData>();
        data.Init(PhotonNetwork.LocalPlayer.NickName);
    }
}
