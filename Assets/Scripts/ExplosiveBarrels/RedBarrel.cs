using UnityEngine;

public abstract class RedBarrel : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;

    private Collider2D[] blownedUpObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blownedUpObjects = Physics2D.OverlapCircleAll(transform.position ,explosionRadius);
        foreach (var blownedUpObject in blownedUpObjects)
        {
            if(blownedUpObject.gameObject.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
            }
            if(blownedUpObject.gameObject.TryGetComponent(out PlayerContoller player))
            {
                player.TakeDamage(damage);
            }
        }
    }
}