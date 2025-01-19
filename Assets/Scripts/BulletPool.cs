using Photon.Pun;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;

    private ObjectPool<BulletController> objectPool;
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
        objectPool = new ObjectPool<BulletController>(
            createFunc: CreateBulletController,
            defaultCapacity: 5,
            actionOnRelease: OnReleaseBullet
        );
    }

    public BulletController SpawnBullet(float damage, bool isFacingRight, Transform spawnPoint, float spreadAngle = 0)
    {
        BulletController bullet = objectPool.Get();
        //StartCoroutine(ReleaseBulletOnTime(bullet));

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation =
            spawnPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-spreadAngle / 2, spreadAngle / 2));

        int bulletID = bullet.GetComponent<PhotonView>().ViewID;
        bullet.InitBulletPool(this);
        photon.RPC(nameof(InitNetwork), RpcTarget.All, damage, isFacingRight, bulletID);
        return bullet;
    }

    public void ReleaseBullet(BulletController bullet)
    {
        if (bullet.gameObject.activeSelf)
        {
            objectPool.Release(bullet);
        }
    }

    [PunRPC]
    private void InitNetwork(float damage, bool isFacingRight, int bulletID)
    {
        BulletController bulletController = PhotonView.Find(bulletID).GetComponent<BulletController>();
        bulletController.gameObject.SetActive(true);
        bulletController.Initializing(damage, isFacingRight);
    }

    private BulletController CreateBulletController()
    {
        return PhotonNetwork.Instantiate(objectToPool.name, Vector2.zero, Quaternion.identity)
            .GetComponent<BulletController>();
    }

    [PunRPC]
    private void SetActiveToBullet(bool isActive, int bulletID, string id)
    {
        BulletController bulletController = PhotonView.Find(bulletID).GetComponent<BulletController>();
        bulletController.gameObject.SetActive(isActive);
    }

    private void OnReleaseBullet(BulletController bullet)
    {
        int bulletID = bullet.GetComponent<PhotonView>().ViewID;
        photon.RPC(nameof(SetActiveToBullet), RpcTarget.All, false, bulletID, PhotonNetwork.LocalPlayer.UserId);
    }
}