using Photon.Pun;
using UnityEngine;

public class MapLoader : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
        PhotonNetwork.LoadLevel("Base");
    }
}
