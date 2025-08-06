using System;
using ExitGames.Client.Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InizializeServerPrefab : MonoBehaviour
{ 
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text players;
    [SerializeField] private TMP_Text isLocked;

    private string roomID;
    private Button connectButton;

    private void Awake()
    {
        connectButton = GetComponentInChildren<Button>();
        connectButton.onClick.AddListener(ConnectRoom);
    }

    public void Init(RoomInfo roomInfo)
    {
        roomID = roomInfo.Name;
        Hashtable roomProperties = roomInfo.CustomProperties;
        roomName.text = roomProperties["name"].ToString();
        UpdatePlayersCount(roomInfo);
        isLocked.text = roomProperties.ContainsKey("password") ? "Locked" : "Unlocked";
    }

    public void UpdatePlayersCount(RoomInfo roomInfo)
    {
        players.text = roomInfo.PlayerCount.ToString() + "/" + roomInfo.MaxPlayers.ToString();
    }

    public void ConnectRoom()
    {
        connectButton.interactable = false;
        RoomManager roomManager = FindFirstObjectByType<RoomManager>();
        roomManager.JoinRoom(roomID);
    }
}