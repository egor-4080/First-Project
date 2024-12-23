using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    private PlayerContoller player;
    private Rigidbody2D rigitbody;
    private BulletPool bulletPool;
    private PhotonView photon;

    private bool isFacing;
    private float damage;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        photon = GetComponent<PhotonView>();
    }

    public void Initializing(float damage, bool isFacing)
    {
        this.damage = damage;
        this.isFacing = isFacing;

        RotateBullet();
    }

    public void InitBulletPool(BulletPool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    private void RotateBullet()
    {
        float speed = this.speed;
        if (!isFacing)
        {
            speed = -speed;
        }
        gameObject.transform.localScale = new Vector3(isFacing ? 1 : -1, 1, 1);
        rigitbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photon.IsMine)
        {
            collision.gameObject.SendMessageUpwards("IsHuman", true, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            bulletPool.ReleaseBullet(this);
        }
    }
}