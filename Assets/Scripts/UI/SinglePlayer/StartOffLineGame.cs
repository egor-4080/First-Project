using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class StartOffLineGame : MonoBehaviourPunCallbacks
{
    public void StartOffLineMode()
    {
        StartCoroutine(nameof(SetOffLineMode));
    }

    public IEnumerator SetOffLineMode()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}
