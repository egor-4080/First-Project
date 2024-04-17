using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;

    [SerializeField] private float throwRate;

    private WaitForSeconds wait = new WaitForSeconds(0.1f);

    private Rigidbody2D objectRigitBody;
    private Collider2D throwingCollider;
    private Poison throwingObjectBaseComponent;
    private GameObject throwedObject;

    private bool isReload = true;
    private int currentObject = 0;
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

        throwingObjectBaseComponent = throwedObject.GetComponent<Poison>();
        objectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
        throwingCollider = throwedObject.GetComponent<Collider2D>();

        throwingObjectBaseComponent.Throw(direction);
        throwingCollider.isTrigger = false;

        StartCoroutine(WaitForGetTriggerObject(throwingObjectBaseComponent));

        yield return new WaitForSeconds(throwRate);
        isReload = true;

    }

    private IEnumerator WaitForGetTriggerObject(Poison throwingObjectBaseComponent)
    {
        while(objectRigitBody.velocity != Vector2.zero)
        {
            yield return wait;
        }
        throwingCollider.isTrigger = true;
        throwingObjectBaseComponent.Initialization(true);
    }

    public void SetValues(GameObject throwedObject, Vector3 direction, int currentObject)
    {
        this.direction = direction;
        this.currentObject = currentObject;
        this.throwedObject = throwedObject;
    }
}