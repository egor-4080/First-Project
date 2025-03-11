using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> currentRooms = new();
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        currentRooms = roomList;
    }
    
    public void CreateRoom(int maxPlayers, string password)
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

        PhotonNetwork.CreateRoom(null, options);
    }

    public void JoinRoom(string roomName, string password = "")
    {
        RoomInfo foundRoom = currentRooms.FirstOrDefault(x => x.Name == roomName && x.RemovedFromList == false);
        if (foundRoom == null)
        {
            //Выкинуть ошибку
            return;
        }
        if (foundRoom.MaxPlayers == foundRoom.PlayerCount)
        {
            //Выкинуть ошибку
            return;
        }

        Hashtable customProperties = foundRoom.CustomProperties;
        if (customProperties.ContainsKey("password") && customProperties["password"].ToString() != password)
        {
            //Выкинуть ошибку
            return;
        }

        PhotonNetwork.JoinRoom(foundRoom.Name);
    }
}
