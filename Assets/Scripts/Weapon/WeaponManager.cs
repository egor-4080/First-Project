using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Weapon startWeapon;
    [SerializeField] private Weapon[] weapons;

    private PhotonView managerPhotonView;

    private void Awake()
    {
        managerPhotonView = GetComponent<PhotonView>();
    }

    public void GiveStartWeapon(int playerID)
    {
        if (CanGiveGun())
        {
            return;
        }
       
        int idWeapon = PhotonNetwork.Instantiate(startWeapon.gameObject.name, Vector2.zero, Quaternion.identity)
            .GetComponent<PhotonView>()
            .ViewID;
        managerPhotonView.RPC(nameof(GiveWeapon), RpcTarget.All, playerID, idWeapon);
    }

    [PunRPC]
    private void GiveWeapon(int idPlayer, int idWeapon)
    {
        PlayerContoller player = PhotonView.Find(idPlayer).GetComponent<PlayerContoller>();
        Weapon weapon = PhotonView.Find(idWeapon).GetComponent<Weapon>();

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