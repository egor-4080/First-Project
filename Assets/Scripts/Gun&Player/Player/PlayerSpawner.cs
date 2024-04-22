using Cinemachine;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private PlayerContoller playerScript;
    public static List<Transform> players { get; private set; } = new();

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Player Conected");
        players = FindObjectsByType<PlayerContoller>(FindObjectsSortMode.None)
            .Select(i => i.GetComponent<Transform>())
            .ToList();
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
            playerScript = player.GetComponent<PlayerContoller>();
            playerScript.Initialization(MainCamera);
            virtualCamera.Follow = player.transform;
        }
    }
}