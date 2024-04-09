using Cinemachine;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private PlayerContoller playerScript;
    private static List<Transform> players = new();

    public static List<Transform> GetPlayers()
    {
        return players;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
            playerScript = player.GetComponent<PlayerContoller>();
            playerScript.Initialization(MainCamera);
            virtualCamera.Follow = player.transform;
            players.Add(player.transform);
        }
    }
}