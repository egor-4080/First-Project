using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class FindServers : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> rooms = new();

    private void Start()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        rooms = roomList;
        print("A");
    }
}
