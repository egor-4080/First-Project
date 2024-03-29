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
    private object[] parameters;

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

        GameObject bulletPrefab = PhotonNetwork.Instantiate(this.bulletPrefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation);
        StartInitializing(bulletPrefab, isFacingRight);
        /*parameters[0] = bulletPrefab;
        parameters[1] = isFacingRight;
        photonView.RPC(nameof(StartInitializing), RpcTarget.All, parameters);*/

        yield return wait;
        isReloaded = true;
    }

    //[PunRPC]
    public void StartInitializing(GameObject bulletPrefab, bool isFacing)
    {
        BulletController bullet = bulletPrefab.GetComponent<BulletController>();
        bullet.Initializing(damage, isFacing, exploison, owner);
    }
}