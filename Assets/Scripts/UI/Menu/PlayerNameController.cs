using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerNameController : MonoBehaviour
{
    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.NickName == "Player")
        {
            PhotonNetwork.LocalPlayer.NickName = "Player";
        }
    }
    
    public void OnNameChanged(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
    }
}
