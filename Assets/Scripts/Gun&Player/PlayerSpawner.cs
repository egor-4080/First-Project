using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            GameObject player = PhotonNetwork.Instantiate(this.player.name, transform.position, Quaternion.identity);
            virtualCamera.Follow = player.transform;
        }
    }
}
