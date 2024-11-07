using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private TMP_Text readyText;

    private Hashtable players = new Hashtable();
    private Player player;

    private void Start()
    {
        player = PhotonNetwork.LocalPlayer;
        if (PhotonNetwork.IsMasterClient)
        {
            readyButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (player.CustomProperties.ContainsKey("isReady") && (bool)player.CustomProperties["isReady"] == true)
        {

        }
    }

    public void ChangeReady()
    {
        //if(players.Contains(player))
        //{
        //    players.Remove(player);
        //}
        //else
        //{
        //    players.Add(player);
        //}
        //players["isReady"] = isReady;
        PhotonNetwork.LocalPlayer.SetCustomProperties(players);
    }
}
