using System.Collections;
using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    
    private Collider2D[] blownedUpObjects;
    private GameObject exploison;
    private WaitForSeconds waitForAnotherExploison = new WaitForSeconds(0.5f);

    private bool isExplosive = true;

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
            if(blownedUpObject.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
            }
            if(blownedUpObject.TryGetComponent(out PlayerContoller player))
            {
                player.TakeDamage(damage);
            }
            if(blownedUpObject.TryGetComponent(out RedBarrel redBarrel))
            {
                if(redBarrel.gameObject != gameObject && isExplosive)
                {
                    StartCoroutine(WaitForAnotherBOOM(redBarrel));
                }
            }
        }
        blownedUpObjects = null;
        gameObject.SetActive(false);
    }

    private IEnumerator WaitForAnotherBOOM(RedBarrel redBarrel)
    {
        isExplosive = false;
        print(redBarrel);
        yield return waitForAnotherExploison;
        redBarrel.Initializing(exploison);
        isExplosive = true;
    }

    public void Initializing(GameObject exploison)
    {
        this.exploison = exploison;
        GetExploison();
    }
}