using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;

    [SerializeField] private float throwRate;

    private WaitForSeconds wait;

    private Rigidbody2D ObjectRigitBody;
    private Collider2D throwingCollider;
    private Poison throwingObjectBaseComponent;
    private GameObject throwedObject;

    private bool isReload;
    private int currentObject;
    private Vector2 direction;

    private void Start()
    {
        isReload = true;
        currentObject = 0;
        wait = new WaitForSeconds(0.1f);
    }

    public void StartCoroutines()
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
        ObjectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
        throwingCollider = throwedObject.GetComponent<Collider2D>();

        throwingObjectBaseComponent.Throw(direction);
        throwingCollider.isTrigger = false;

        //inventory.RemoveAt(0);

        StartCoroutine(GetMaxDrag(throwingObjectBaseComponent));

        yield return new WaitForSeconds(throwRate);
        isReload = true;

    }

    public IEnumerator GetMaxDrag(Poison throwingObjectBaseComponent)
    {
        while(ObjectRigitBody.velocity != Vector2.zero)
        {
            yield return wait;
        }
        throwingCollider.isTrigger = true;
        throwingObjectBaseComponent.Initialization(true);
    }

    public void SetValues(GameObject throwedObject, Vector3 direction, int currentObject)
    {
        this.currentObject = currentObject;
        this.direction = direction;
        this.throwedObject = throwedObject;
    }
}