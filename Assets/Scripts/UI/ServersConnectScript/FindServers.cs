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
    
    private List<RoomInfo> rooms = new();
    private List<GameObject> currentRooms = new List<GameObject>();
    private bool isStarted = false;

    private ScrollBarController scrollBarController;

    private void Awake()
    {
        scrollBarController = GetComponent<ScrollBarController>();
    }

    private void Start()
    {
        StartCoroutine(UpdateContent());
    }

    public void StopSearching()
    {
        isStarted = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        rooms = roomList;
    }

    private IEnumerator UpdateContent()
    {
        while (!isStarted)
        {
            foreach (var currentRoom in currentRooms)
            {
                Destroy(currentRoom.gameObject);
            }
            currentRooms.Clear();

            foreach (var room in rooms)
            {
                print(rooms.Count);
                if (room.RemovedFromList)
                    continue;
                GameObject roomObject = Instantiate(roomPrefab, transform);
                currentRooms.Add(roomObject);
            }
            
            scrollBarController.SetHeightContent(currentRooms.Count);
            yield return new WaitForSeconds(3f);
        }
    }
    
    private void PrefabInit()
    {
        
    }
}
