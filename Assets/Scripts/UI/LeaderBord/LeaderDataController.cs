using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LeaderDataController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<PlayerData> playersDatas = new();
    
    private PhotonView photon;

    private int counerPosition = 0;

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
        foreach (var data in playersDatas)
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
        photon.RPC(nameof(NetworkChange), RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (var data in playersDatas)
        {
            if (!data)
            {
                PhotonNetwork.Destroy(data.gameObject);
                break;
            }
        }
        photon.RPC(nameof(GetDatas), RpcTarget.All);
    }

    private void SortDatas()
    {
        PlayerData temp;
        for (int i = 0; i < playersDatas.Count - 1; i++)
        {
            for (int j = 0; j < playersDatas.Count - 1 - i; j++)
            {
                if (playersDatas[j].Score > playersDatas[j + 1].Score)
                {
                    temp = playersDatas[j];
                    playersDatas[j] = playersDatas[j + 1];
                    playersDatas[j + 1] = temp;
                }
            }
        }
        playersDatas.Reverse();
    }

    [PunRPC]
    private void NetworkChange()
    {
        SortDatas();
        foreach (var data in playersDatas)
        {
            data.transform.SetParent(null);
        }

        foreach (var data in playersDatas)
        {
            data.transform.SetParent(transform);
        }
    }

    [PunRPC]
    private void GetDatas()
    {
        playersDatas.Clear();
        foreach(Transform child in transform)
        {
            playersDatas.Add(child.GetComponent<PlayerData>());
        }
    }

    private void MakeData()
    {
        PlayerData data = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<PlayerData>();
        data.Init(PhotonNetwork.LocalPlayer.NickName, this, counerPosition);
        photon.RPC(nameof(ChangeCounter), RpcTarget.All);
    }

    [PunRPC]
    private void ChangeCounter()
    {
        counerPosition -= 50;
    }
}
