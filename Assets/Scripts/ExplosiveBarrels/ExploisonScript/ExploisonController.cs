using UnityEngine;

public class ExploisonController : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    [SerializeField] float destroyTime;

    private Collider2D[] blownedUpObjects;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void GetDamageExploison()
    {
        blownedUpObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var blownedUpObject in blownedUpObjects)
        {
            if (blownedUpObject.TryGetComponent(out Character character))
            {
                character.TakeDamage(damage);
            }
            if (blownedUpObject.TryGetComponent(out Barrel barrel))
            {
                barrel.Initializing();
            }
        }
        blownedUpObjects = null;
    }
}