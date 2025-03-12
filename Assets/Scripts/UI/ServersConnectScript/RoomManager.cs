using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField connectPassword;
    [SerializeField] private TMP_InputField connectID;
    [SerializeField] private Button connectButton;
    
    private List<RoomInfo> currentRooms = new();
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        currentRooms = roomList;
    }
    
    public void CreateRoom(int maxPlayers, string password = "")
    {
        Hashtable room = new Hashtable();
        if (password != "")
        {
            room.Add("password", password);
        }
        room.Add("name", PhotonNetwork.LocalPlayer.NickName);
        
        RoomOptions options = new RoomOptions()
        {
            MaxPlayers = maxPlayers,
            CustomRoomProperties = room,
        };

        PhotonNetwork.CreateRoom(GiveRoomID(), options);
    }

    private string GiveRoomID()
    {
        List<int> names = currentRooms.Where(x => x.RemovedFromList == false)
            .Select(x => int.Parse(x.Name)).ToList();
        
        int preID = names.Count;
        while (true)
        {
            if (!names.Contains(preID))
            {
                Debug.Log($"Room has been created with ID: {preID}");
                return preID.ToString();
            }
            preID++;
        }
    }

    public void FindFreeRoom()
    {
        var currentRooms = this.currentRooms
            .Where(x => x.RemovedFromList == false && x.PlayerCount != x.MaxPlayers).ToList();
        if (currentRooms.Count != 0)
            PhotonNetwork.JoinRandomRoom();
        else
            CreateRoom(4);
    }

    public void JoinRoom()
    {
        RoomInfo foundRoom = currentRooms.FirstOrDefault(x => x.Name == connectID.text && x.RemovedFromList == false);
        if (foundRoom == null)
        {
            //Выкинуть ошибку
            connectButton.interactable = true;
            return;
        }
        if (foundRoom.MaxPlayers == foundRoom.PlayerCount)
        {
            //Выкинуть ошибку
            connectButton.interactable = true;
            return;
        }

        Hashtable customProperties = foundRoom.CustomProperties;
        if (customProperties.ContainsKey("password") && customProperties["password"].ToString() != connectPassword.text)
        {
            //Выкинуть ошибку
            connectButton.interactable = true;
            return;
        }
        
        PhotonNetwork.JoinRoom(foundRoom.Name);
    }
}
