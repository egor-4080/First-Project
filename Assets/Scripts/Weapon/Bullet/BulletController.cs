using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigitbody;

    private bool isFacing;
    private float damage;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Initializing(float damage, bool isFacing)
    {
        this.damage = damage;
        this.isFacing = isFacing;

        Invoke(nameof(OffBullet), 0.28f);
        RotateBullet();
    }

    private void RotateBullet()
    {
        float speed = this.speed;
        if (!isFacing)
        {
            gameObject.transform.localScale = new Vector3(isFacing ? 1 : -1, 1, 1);
            speed = -speed;
        }
        rigitbody.velocity = transform.right * speed;
    }

    private void OffBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(false);
    }
}