using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private PlayerContoller playerScript;

    private void Start()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            GameObject player = PhotonNetwork.Instantiate(this.player.name, transform.position, Quaternion.identity);
            playerScript = player.GetComponent<PlayerContoller>();
            playerScript.Initialization(MainCamera);
            virtualCamera.Follow = player.transform;
        }
    }
}