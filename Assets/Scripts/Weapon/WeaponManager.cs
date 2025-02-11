using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Weapon startWeapon;
    [SerializeField] private Weapon[] weapons;

    private Weapon currentWeapon;
    private GameObject equipedWeapon;
    private PlayerContoller ownerPlayerConrollter;
    private PhotonView managerPhotonView;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        managerPhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        currentWeapon = startWeapon;
    }

    public void SetOwnerPlayer(PlayerContoller ownerPlayer)
    {
        ownerPlayerConrollter = ownerPlayer;
    }

    public void GiveWeapon()
    {
        if (CanGiveGun())
        {
            return;
        }

        var weapon =
            PhotonNetwork.Instantiate(currentWeapon.gameObject.name, Vector2.zero, Quaternion.identity);
        equipedWeapon = weapon;
        var weaponPhotonView = weapon.GetComponent<PhotonView>();
        var idWeapon = weaponPhotonView.ViewID;
        CreateSwapWeaponSound();

        var playerId = ownerPlayerConrollter.GetComponent<PhotonView>().ViewID;
        managerPhotonView.RPC(nameof(EquipWeapon), RpcTarget.AllBuffered, playerId, idWeapon);
    }
    
    public void GiveWeapon(GameObject newWeapon)
    {
        currentWeapon = newWeapon.GetComponent<Weapon>();
        GiveWeapon();
    }

    public void CreateSwapWeaponSound()
    {
        audioSource.Play();
    }

    [PunRPC]
    private void EquipWeapon(int idPlayer, int idWeapon)
    {
        PhotonView playerView = PhotonView.Find(idPlayer);
        PhotonView weaponView = PhotonView.Find(idWeapon);
        
        if (playerView == null || weaponView == null)
        {
            return;
        }
        
        PlayerContoller player = playerView.GetComponent<PlayerContoller>();
        Weapon weapon = weaponView.GetComponent<Weapon>();
        player.SetWeapon(weapon);
    }

    private bool CanGiveGun()
    {
        string scene = SceneManager.GetActiveScene().name;
        foreach (var name in Config.instance.noGunScenes)
        {
            if (scene == name)
            {
                return true;
            }
        }

        return false;
    }
}