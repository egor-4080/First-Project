using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Invoke(nameof(WaitForGetDatas), 0.1f);
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
        playersDatas = playersDatas.OrderByDescending(p => p.Score).ToList();
    }

    [PunRPC]
    private void NetworkChange()
    {
        SortDatas();
        int counter = 0;
        foreach (var playerData in playersDatas)
        {
            RectTransform dataTransform = playerData.GetComponent<RectTransform>();
            dataTransform.anchoredPosition = new Vector3(0, counter);
            counter -= 50;
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
        //Не вызываются метод
        NetworkChange();
    }

    private void WaitForGetDatas()
    {
        photon.RPC(nameof(GetDatas), RpcTarget.All);
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
