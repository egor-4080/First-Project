using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject exploison;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float reloadSpeed;

    private WaitForSeconds wait;
    private bool isReloaded;

    //RPC
    private bool isFacingRight;
    private GameObject bulletGameObject;

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

        GameObject bulletGameObject = PhotonNetwork.Instantiate(this.bulletPrefab.name, spawnPoint.transform.position, spawnPoint.transform.rotation);
        this.bulletGameObject = bulletGameObject;

        RunRPC();

        yield return wait;
        isReloaded = true;
    }

    private void RunRPC()
    {
        photonView.RPC(nameof(StartInitializing), RpcTarget.Others);
    }

    [PunRPC]
    public void StartInitializing()
    {
        print("AAA");
        BulletController bullet = bulletGameObject.GetComponent<BulletController>();
        bullet.Initializing(damage, isFacingRight, exploison, owner);
    }
}