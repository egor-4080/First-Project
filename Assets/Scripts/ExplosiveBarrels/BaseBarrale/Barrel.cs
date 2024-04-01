using UnityEngine;

public abstract class Barrel : MonoBehaviour
{   
    [SerializeField] private GameObject exploisonPrefab;

    protected ExploisonController explosionController;
    private GameObject exploison;

    private void GetExploison()
    {
        exploison = Instantiate(exploisonPrefab, transform.position, Quaternion.identity);
        MakeExploisonEffect(exploison);
    }

    virtual public void MakeExploisonEffect(GameObject exploison)
    {
        Destroy(gameObject);
        explosionController = exploison.GetComponent<ExploisonController>();
    }

    public void Initializing()
    {
        GetExploison();
    }
}