using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveGame : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (!PhotonNetwork.IsConnected) 
            Destroy(gameObject);
    }

    public void OnLeaveTheGame()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected");
        SceneManager.LoadScene("Menu");
    }
}
