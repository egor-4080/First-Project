using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject exploison;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float reloadSpeed;

    private WaitForSeconds wait;
    private bool isReloaded;

    private void Start()
    {
        wait = new WaitForSeconds(reloadSpeed);
        isReloaded = true;
    }

    public override void Fire(bool isFacingRight)
    {
        if (isReloaded)
        {
            StartCoroutine(SpawnBullet(isFacingRight));
            base.Fire(isFacingRight);
        }
    }

    private IEnumerator SpawnBullet(bool isFacingRight)
    {
        isReloaded = false;

        BulletController bullet = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation)
            .GetComponent<BulletController>();
        photonView.RPC(nameof(StartInitializing), RpcTarget.All, bullet, isFacingRight);

        yield return wait;
        isReloaded = true;
    }

    [PunRPC]
    public void StartInitializing(BulletController bullet, bool isFacing)
    {
        bullet.Initializing(damage, isFacing, exploison, owner);
    }
}