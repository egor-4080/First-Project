using UnityEngine;

public class ExploisonController : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    [SerializeField] float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void DamageExploison()
    {
        Collider2D[] blownedUpObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var blownedUpObject in blownedUpObjects)
        {
            blownedUpObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}