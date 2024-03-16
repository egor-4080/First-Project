using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float forceSpeed;

    private Collider2D currentCollider;
    private Rigidbody2D rigitBody;

    protected bool isBreak;

    private void Awake()
    {
        currentCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector3 direction)
    {
        rigitBody.velocity = direction * forceSpeed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        DoWhenObjectBreak();
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
        }
        currentCollider.isTrigger = true;
    }

    public void DoWhenObjectBreak()
    {
        if (!isBreak)
        {
            isBreak = true;
        }
    }
}