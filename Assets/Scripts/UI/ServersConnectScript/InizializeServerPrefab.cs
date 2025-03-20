using ExitGames.Client.Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InizializeServerPrefab : MonoBehaviour
{ 
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text players;
    [SerializeField] private TMP_Text isLocked;

    public void Init(RoomInfo roomInfo)
    {
        Hashtable roomProperties = roomInfo.CustomProperties;
        //name.text = roomProperties["name"].ToString();
        roomName.text = "player";
        UpdatePlayersCount(roomInfo);
        isLocked.text = roomProperties.ContainsKey("password") ? "Locked" : "Unlocked";
    }

    public void UpdatePlayersCount(RoomInfo roomInfo)
    {
        players.text = roomInfo.PlayerCount.ToString() + "/" + roomInfo.MaxPlayers.ToString();
    }
}
