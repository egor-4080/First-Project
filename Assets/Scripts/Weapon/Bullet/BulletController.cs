using System.Collections;
using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private BulletPool bulletPool;
    private float damage;

    private bool isFacing;
    private PhotonView photon;

    private PlayerContoller player;
    private Rigidbody2D rigitbody;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        photon = GetComponent<PhotonView>();
    }

    public void OnEnable()
    {
        if (photon.IsMine)
        {
            StartCoroutine(nameof(WaitForDespawn));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photon.IsMine)
        {
            StopCoroutine(nameof(WaitForDespawn));
            collision.gameObject.SendMessageUpwards("IsHuman", true, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            bulletPool.ReleaseBullet(this);
        }
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
        rigitbody.linearVelocity = transform.right * speed;
    }

    private IEnumerator WaitForDespawn()
    {
        yield return new WaitForSeconds(0.28f);
        bulletPool.ReleaseBullet(this);
    }
}