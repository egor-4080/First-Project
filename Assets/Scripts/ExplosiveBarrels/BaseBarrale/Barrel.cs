using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    
    private Collider2D[] blownedUpObjects;
    private GameObject exploison;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void GetExploison()
    {
        blownedUpObjects = Physics2D.OverlapCircleAll(transform.position ,explosionRadius);
        Instantiate(exploison, transform.position, Quaternion.Euler(0, 0, 0));
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
        gameObject.SetActive(false);
    }

    public void Initializing(GameObject exploison)
    {
        this.exploison = exploison;
        GetExploison();
    }
}