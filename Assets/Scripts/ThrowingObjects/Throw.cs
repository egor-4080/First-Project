using System.Collections;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] private float throwRate;

    private Collider2D throwingCollider;
    private ThrowingObjectController throwingObjectBaseComponent;
    private GameObject throwedObject;

    private bool isReload = true;
    private Vector2 direction;

    public void ThrowObject()
    {
        if (isReload)
        {
            StartCoroutine(SpawnWithRate());
        }
    }

    private IEnumerator SpawnWithRate()
    {
        isReload = false;

        throwedObject.SetActive(true);
        throwedObject.transform.SetParent(null);

        throwingObjectBaseComponent = throwedObject.GetComponent<ThrowingObjectController>();
        throwingCollider = throwedObject.GetComponent<Collider2D>();

        throwingObjectBaseComponent.Throw(direction);
        throwingCollider.isTrigger = false;

        yield return new WaitForSeconds(throwRate);
        isReload = true;
    }

    public void SetValues(GameObject throwedObject, Vector3 direction)
    {
        this.direction = direction;
        this.throwedObject = throwedObject;
    }
}