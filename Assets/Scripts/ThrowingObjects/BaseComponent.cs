using UnityEngine;

public class BaseComponent : ThrowAndTake
{
    [SerializeField] private float damage;

    private Collider2D currentCollider;
    private Rigidbody2D rigitBody;

    private void Awake()
    {
        currentCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();

        rigitBody.drag = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
            currentCollider = GetComponent<Collider2D>();
            currentCollider.isTrigger = true;
        }
        if(collision.gameObject.TryGetComponent(out PlayerContoller playerController))
        {
            currentCollider.isTrigger = true;
        }
    }
}