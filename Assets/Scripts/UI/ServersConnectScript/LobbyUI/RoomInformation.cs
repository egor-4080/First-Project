using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    [SerializeField] private GameObject roomInfo;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text roomPassword;
    
    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(roomInfo);
            return;
        }

        SetRoomInfoUI();
    }

    private void SetRoomInfoUI()
    {
        RoomInfo cuurentRoomInfo = PhotonNetwork.CurrentRoom;
        Hashtable roomProperties = cuurentRoomInfo.CustomProperties;
        roomName.text = cuurentRoomInfo.Name;
        if (roomProperties.ContainsKey("password"))
        {
            roomPassword.text = roomProperties["password"].ToString();
        }
    }
}
