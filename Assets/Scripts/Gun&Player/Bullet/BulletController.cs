using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [HideInInspector] public bool isFacing;
    [HideInInspector] public float inDamage;

    private Rigidbody2D rigitbody;
    private float outDamage;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        outDamage = inDamage;
        if (!isFacing)
        {
            gameObject.transform.localScale = new Vector3(isFacing ? 1 : -1, 1, 1);
            speed = 1 * -speed;
        }
        rigitbody.velocity = transform.right * speed;
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(outDamage);
            Destroy(gameObject);
        }
    }
}
