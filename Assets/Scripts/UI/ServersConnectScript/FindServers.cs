using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class FindServers : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomPrefab;
    
    private Dictionary<string, GameObject> rooms = new();

    private ScrollBarController scrollBarController;

    private void Awake()
    {
        scrollBarController = GetComponent<ScrollBarController>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (!rooms.ContainsKey(room.Name) && !room.RemovedFromList)
            {
                GameObject newRoom = Instantiate(roomPrefab, transform);
                rooms.Add(room.Name, newRoom);
                PrefabInit(room);
                continue;
            }
            
            if (room.RemovedFromList && rooms.ContainsKey(room.Name))
            {
                rooms.Remove(room.Name, out GameObject deactiveRoom);
                Destroy(deactiveRoom);
                continue;
            }
            scrollBarController.SetHeightContent(rooms.Count);
            
            Debug.Log($"Update rooms info:{room.Name}");
        }
    }
    
    private void PrefabInit(RoomInfo room)
    {
        
    }
}
