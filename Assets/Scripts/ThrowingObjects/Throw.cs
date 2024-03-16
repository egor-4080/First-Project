using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] private List<GameObject> throwingObjects;

    [SerializeField] private float throwRate;

    private WaitForSeconds wait;

    private Rigidbody2D ObjectRigitBody;
    private Collider2D throwingCollider;
    private BaseComponent throwingObjectBaseComponent;
    private GameObject throwedObject;

    private bool isReload;
    private Vector2 direction;

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
            throwedObject.SetActive(true);
            throwedObject.transform.SetParent(null);

            throwingObjectBaseComponent = throwedObject.GetComponent<BaseComponent>();
            ObjectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
            throwingCollider = throwedObject.GetComponent<Collider2D>();

            throwingObjectBaseComponent.Throw(direction);
            throwingCollider.isTrigger = false;

            throwingObjects.RemoveAt(0);

            StartCoroutine(GetMaxDrag(throwingObjectBaseComponent));

            yield return new WaitForSeconds(throwRate);
            isReload = true;
        }
    }

    private IEnumerator GetMaxDrag(BaseComponent throwingObjectBaseComponent)
    {
        for (int i = 0; i < 1000; i++)
        {
            i *= 2;
            ObjectRigitBody.drag = i;
            yield return wait;
        }
        throwingCollider.isTrigger = true;
        throwingObjectBaseComponent.DoWhenObjectBreak();
    }

    public void SetValues(int i, List<GameObject> throwingObjects, Vector3 direction)
    {
        this.direction = direction;
        this.throwingObjects = throwingObjects;
    }
}