using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAndTake : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] private List<GameObject> throwingObjects;

    [SerializeField] private float throwRate;

    private WaitForSeconds wait;

    private Rigidbody2D ObjectRigitBody;
    private Collider2D throwingCollider;
    private BaseComponent throwingObjectBaseComponent;
    private GameObject throwedObject;
    private Transform throwingTransform;

    private bool isReload;
    private int lookRotation;

    private void Start()
    {
        isReload = true;
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
        if (throwingObjects.Count != 0)
        {
            isReload = false;

            throwedObject = throwingObjects[0];
            throwingTransform = throwedObject.transform;

            throwingTransform.position = throwingTransformPosition.position;
            throwingTransform.rotation = throwingTransformPosition.rotation;

            throwingObjectBaseComponent = throwedObject.GetComponent<BaseComponent>();
            ObjectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
            throwingCollider = throwedObject.GetComponent<Collider2D>();

            throwingObjectBaseComponent.SetRotation(lookRotation);
            throwingCollider.isTrigger = false;

            throwingObjects.RemoveAt(0);

            StartCoroutine(GetMaxDrag());

            yield return new WaitForSeconds(throwRate);
            isReload = true;
        }
    }

    private IEnumerator GetMaxDrag()
    {
        for (int i = 0; i < 100000; i++)
        {
            i *= 2;
            ObjectRigitBody.drag = i;
            yield return wait;
        }
        throwingCollider.isTrigger = true;
    }

    public void SetValues(int i, List<GameObject> throwingObjects)
    {
        lookRotation = i;
        this.throwingObjects = throwingObjects;
    }
}