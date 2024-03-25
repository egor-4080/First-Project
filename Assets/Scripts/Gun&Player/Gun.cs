using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bullet;
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

        PhotonNetwork.Instantiate(bullet.name, spawnPoint.transform.position, spawnPoint.transform.rotation)
            .GetComponent<BulletController>()
            .Initializing(damage, isFacingRight, exploison);

        yield return wait;
        isReloaded = true;
    }
}