using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float forceSpeed;

    private Collider2D currentCollider;
    private Rigidbody2D rigitBody;

    private int throwRotation;

    private void Awake()
    {
        currentCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    public void SetRotation(int i)
    {
        throwRotation = i;
    }

    private void Start()
    {
        rigitBody.drag = 0;
    }

    private void FixedUpdate()
    {
        if (!currentCollider.isTrigger)
        {
            rigitBody.velocity = transform.right * forceSpeed * throwRotation;
        }
        else
        {
            rigitBody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
            currentCollider = GetComponent<Collider2D>();
        }
        currentCollider.isTrigger = true;
    }
}