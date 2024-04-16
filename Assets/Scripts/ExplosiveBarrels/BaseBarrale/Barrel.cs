using UnityEngine;

public abstract class Barrel : MonoBehaviour
{   
    [SerializeField] private GameObject exploisonPrefab;

    public void Exployed()
    {
        Instantiate(exploisonPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}