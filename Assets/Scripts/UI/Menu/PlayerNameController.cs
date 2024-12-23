using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerNameController : MonoBehaviour
{
    public void OnNameChanged(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
    }
}
