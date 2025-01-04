using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : Weapon
{
    protected override void Fire(bool isFacing)
    {
        base.Fire(isFacing);
        objectPool.SpawnBullet(damage, isFacing, spawnPoint, spreadAngle);
    }
}