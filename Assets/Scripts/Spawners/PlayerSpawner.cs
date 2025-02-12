using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PlayerLifesController playerLifes;
    [SerializeField] private MenuSwitcher menuSwitcher;
    [SerializeField] private Transform content;
    [SerializeField] private WeaponManager weaponManager;

    private PlayerContoller playerScript;
    private PhotonView photon;
    private float deathTimer = 5;

    public static List<Transform> players { get; private set; } = new();

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected != true) return;
        Player player = PhotonNetwork.LocalPlayer;
        Hashtable playerProperties = new Hashtable();
        playerProperties["Score"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        Respawn();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GetPlayers();
    }

    public void PlayerRespawn()
    {
        playerLifes.TakeLifeAway();
        if (playerLifes.GetCurrentLifes() == 0)
        {
            //доделать
        }
        Invoke(nameof(Respawn), deathTimer);
    }

    [PunRPC]
    private void GetPlayers()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            players = FindObjectsByType<PlayerContoller>(FindObjectsSortMode.None)
                .Select(i => i.GetComponent<Transform>())
                .ToList();
        }
    }

    private void Respawn()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
        playerScript = player.GetComponent<PlayerContoller>();
        menuSwitcher.SetPlayerController(playerScript);
        playerScript.Initialization(MainCamera, this, content);
        virtualCamera.Follow = player.transform;
        players.Add(player.transform);
        AudioManager audioManager = AudioManager.instance;
        weaponManager.SetOwnerPlayer(playerScript);
        weaponManager.GiveWeapon();
        photon.RPC(nameof(GetPlayers), RpcTarget.MasterClient);
        audioManager.OnNewAudiosAppeared();
    }
}