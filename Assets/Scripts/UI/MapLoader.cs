using Photon.Pun;

public class MapLoader : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.SendRate = 120;
        PhotonNetwork.SerializationRate = 120;
        PhotonNetwork.LoadLevel("Base");
    }
}
