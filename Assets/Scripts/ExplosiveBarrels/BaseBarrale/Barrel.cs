using UnityEngine;

public abstract class Barrel : MonoBehaviour
{   
    [SerializeField] private GameObject exploisonPrefab;

    private GameObject exploison;

    public void GetExploison()
    {
        exploison = Instantiate(exploisonPrefab, transform.position, Quaternion.identity);
        MakeExploisonEffect(exploison);
    }

    abstract public void MakeExploisonEffect(GameObject exploison);

    public void Initializing()
    {
        GetExploison();
    }
}