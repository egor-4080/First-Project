using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bullet;
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

        Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation)
            .GetComponent<BulletController>()
            .Initializing(damage, isFacingRight);

        yield return wait;
        isReloaded = true;
    }
}