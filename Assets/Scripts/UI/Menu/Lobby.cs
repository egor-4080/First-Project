using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Menu menu;

    private void Awake()
    {
        menu.SetScreen(Menu.Screens.Connect);
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        menu.SetScreen(Menu.Screens.Wait);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        menu.SetScreen(Menu.Screens.Rooms);
    }
}
