using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using YG;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Menu menu;

    private void Awake()
    {
        menu.SetScreen(Menu.Screens.Connect);
        YG2.onGetSDKData += () => PhotonNetwork.LocalPlayer.NickName = YG2.player.name;
    }

    public void Connect()
    {
        YG2.OpenAuthDialog();
        PhotonNetwork.ConnectUsingSettings();
        menu.SetScreen(Menu.Screens.Wait);
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        menu.SetScreen(Menu.Screens.Rooms);
    }
}
