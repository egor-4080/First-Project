using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    public void CreateRoom(int maxPlayers, string password)
    {
        Hashtable room = new Hashtable();
        if (password != null)
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
}
