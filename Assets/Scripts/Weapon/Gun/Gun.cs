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

        BulletController bulletController = objectPool.SpawnBullet(damage, isFacingRight, spawnPoint);

        yield return wait;
        isReloaded = true;
    }
}