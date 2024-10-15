using Photon.Pun;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public void QuickGame()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}
