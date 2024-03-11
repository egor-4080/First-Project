using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float forceSpeed;

    private Collider2D currentCollider;
    private Rigidbody2D rigitBody;

    private void Awake()
    {
        currentCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector3 direction)
    {
        rigitBody.velocity = direction * forceSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
        }
        currentCollider.isTrigger = true;
    }
}