using UnityEngine;

public class BaseComponent : ThrowAndTake
{
    [SerializeField] private float damage;

    private Collider2D currentCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
            currentCollider = GetComponent<Collider2D>();
            currentCollider.isTrigger = true;
        }
    }
}