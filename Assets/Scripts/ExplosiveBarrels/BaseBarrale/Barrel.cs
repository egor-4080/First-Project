using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    
    private Collider2D[] blownedUpObjects;
    private ExploisonController exploison;

    private void Start()
    {
        exploison = GetComponentInChildren<ExploisonController>();
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D()
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
        blownedUpObjects = null;
        exploison.SetExploisenStatus();
    }

    public void SetBarrelStatus()
    {
        exploison.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}