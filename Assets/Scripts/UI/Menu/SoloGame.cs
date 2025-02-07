using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloGame : MonoBehaviour
{
    public void OnPressed()
    {
        SceneManager.LoadScene("Lobby");
    }
}