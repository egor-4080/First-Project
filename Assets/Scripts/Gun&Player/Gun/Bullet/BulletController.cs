using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigitbody;
    private GameObject exploison;
    private Character owner;

    private bool isFacing;
    private float damage;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!isFacing)
        {
            gameObject.transform.localScale = new Vector3(isFacing ? 1 : -1, 1, 1);
            speed = 1 * -speed;
        }
        rigitbody.velocity = transform.right * speed;
        Destroy(gameObject, 2);
    }

    public void Initializing(float damage, bool isFacing, GameObject exploison, Character owner)
    {
        this.owner = owner;
        this.exploison = exploison;
        this.damage = damage;
        this.isFacing = isFacing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
