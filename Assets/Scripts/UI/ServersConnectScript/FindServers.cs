using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class FindServers : MonoBehaviourPunCallbacks
{
    [SerializeField] private InizializeServerPrefab roomPrefab;
    
    private Dictionary<string, InizializeServerPrefab> rooms = new();

    private ScrollBarController scrollBarController;

    private void Awake()
    {
        scrollBarController = GetComponent<ScrollBarController>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomList.Select(room => room.IsOpen).ToList();
        foreach (var room in roomList)
        {
            Debug.Log(room.CustomProperties.ContainsKey("name"));
            if (!rooms.ContainsKey(room.Name) && !room.RemovedFromList)
            {
                InizializeServerPrefab newRoom = Instantiate(roomPrefab, transform);
                rooms.Add(room.Name, newRoom);
                newRoom.Init(room);
                scrollBarController.SetHeightContent(rooms.Count);
                continue;
            }
            
            if (room.RemovedFromList && rooms.ContainsKey(room.Name))
            {
                rooms.Remove(room.Name, out InizializeServerPrefab deactiveRoom);
                Destroy(deactiveRoom.gameObject);
                scrollBarController.SetHeightContent(rooms.Count);
                continue;
            }
            scrollBarController.SetHeightContent(rooms.Count);
            InizializeServerPrefab serverScript = rooms[room.Name];
            serverScript.UpdatePlayersCount(room);
            
            Debug.Log($"Update rooms info:{room.Name}");
        }
    }
}
