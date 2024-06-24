using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float reloadSpeed;

    private WaitForSeconds wait;
    private bool isReloaded;
    private bool isFacingRight;

    private void Start()
    {
        wait = new WaitForSeconds(reloadSpeed);
        isReloaded = true;
    }

    public override void Fire(bool isFacingRight)
    {
        if (isReloaded)
        {
            base.Fire(isFacingRight);
            this.isFacingRight = isFacingRight;
            StartCoroutine(SpawnBullet());
        }
    }

    private IEnumerator SpawnBullet()
    {
        isReloaded = false;

        GameObject bulletGameObject = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation);

        PhotonView photonView = bulletGameObject.GetComponent<PhotonView>();
        int id = photonView.ViewID;

        this.photonView.RPC(nameof(Bullet), RpcTarget.All, damage, isFacingRight, id);

        yield return wait;
        isReloaded = true;
    }

    [PunRPC]
    public void Bullet(float damage, bool isFacingRight, int id)
    {
        PhotonView photonBullet = PhotonView.Find(id);

        BulletController bullet = photonBullet.GetComponent<BulletController>();
        bullet.Initializing(damage, isFacingRight);
    }
}