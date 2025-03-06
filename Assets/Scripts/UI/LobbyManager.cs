using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using NUnit.Framework;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private TMP_Text readyText;
    [SerializeField] private TMP_Text countText;
    
    private PhotonView photon;
    private Player player;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        player = PhotonNetwork.LocalPlayer;
        Hashtable playerProperties = new Hashtable();
        if (PhotonNetwork.IsMasterClient)
        {
            playerProperties["IsReady"] = true;
            readyButton.SetActive(false);
        }
        else
        {
            playerProperties["IsReady"] = false;
            startButton.SetActive(false);
        }
        player.SetCustomProperties(playerProperties);
        Invoke(nameof(UpdateReadiesCount), 0.5f);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Invoke(nameof(UpdateReadiesCount), 0.5f);
    }

    public void StartGame()
    {
        (int readyPlayers, int countPlayers) = GetReadyPlayers();
        if (readyPlayers == countPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            photon.RPC(nameof(LoadScene), RpcTarget.All);
        }
    }

    [PunRPC]
    private void LoadScene()
    {
        PhotonNetwork.LoadLevel("Base");
    }

    public void ChangeReady()
    {
        Hashtable newPlayerProperties = player.CustomProperties;
        newPlayerProperties["IsReady"] = !(bool)newPlayerProperties["IsReady"];

        player.SetCustomProperties(newPlayerProperties);
        readyText.text = (bool)newPlayerProperties["IsReady"] ? "Cancel" : "Ready";

        photon.RPC(nameof(UpdateReadiesCount), RpcTarget.All);
    }

    [PunRPC]
    private void UpdateReadiesCount()
    {
        (int redyPlayers, int countPlayers) = GetReadyPlayers();
        countText.text = redyPlayers.ToString() + "/" + countPlayers.ToString();
    }

    private (int, int) GetReadyPlayers()
    {
        var players = PhotonNetwork.PlayerList;
        int count = 0;

        foreach (var player in players)
            if (player.CustomProperties["IsReady"] != null && (bool)player.CustomProperties["IsReady"] )
                count++;

        return (count, players.Length);
    }
}
