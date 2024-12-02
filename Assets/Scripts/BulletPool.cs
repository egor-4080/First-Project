using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;

    private ObjectPool<BulletController> objectPool;
    private PhotonView photon;

    private void Awake()
    {
        objectPool = new ObjectPool<BulletController>(
            createFunc: CreateBulletController,
            defaultCapacity: 5
            );
    }

    private BulletController CreateBulletController()
    {
        return PhotonNetwork.Instantiate(objectToPool.name, Vector2.zero, Quaternion.identity)
            .GetComponent<BulletController>();
    }

    public BulletController GetBullet()
    {
        BulletController bullet = objectPool.Get();
        StartCoroutine(ReleaseBullet(bullet));
        return bullet;
    }

    private IEnumerator ReleaseBullet(BulletController bullet)
    {
        yield return new WaitForSeconds(0.28f);
        objectPool.Release(bullet);
    }
}
