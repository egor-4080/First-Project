using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : Weapon
{
    private WaitForSeconds wait;
    private bool isReloaded;
    private bool isFacingRight;

    void Start()
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

        photonView.RPC(nameof(Bullet), RpcTarget.All, damage, isFacingRight);

        yield return wait;
        isReloaded = true;
    }

    [PunRPC]
    public void Bullet(float damage, bool isFacingRight)
    {
        BulletController bulletController = objectPool.GetPooledObject();
        bulletController.gameObject.SetActive(true);

        bulletController.transform.position = spawnPoint.position;
        bulletController.transform.rotation = spawnPoint.rotation;
        bulletController.Initializing(damage, isFacingRight);
    }
}