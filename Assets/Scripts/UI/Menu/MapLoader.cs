using Photon.Pun;

public class MapLoader : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.SendRate = 165;
        PhotonNetwork.SerializationRate = 165;
        PhotonNetwork.LoadLevel("Base");
    }
}
