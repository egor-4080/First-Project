using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class ThrowAndTake : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] protected List<GameObject> throwingObjects;

    [SerializeField] private float throwRate;

    protected WaitForSeconds wait;

    private Rigidbody2D ObjectRigitBody;
    protected Collider2D throwingCollider;
    private GameObject throwedObject;

    protected bool isTake;
    private bool isReload;
    private int mathForce;

    protected virtual void Start()
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

    public IEnumerator SpawnWithRate()
    {
        if (throwingObjects.Count != 0)
        {
            isReload = false;

            throwedObject = Instantiate(throwingObjects[0], throwingTransformPosition.position, throwingTransformPosition.rotation);
            ObjectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
            throwingCollider = throwedObject.GetComponent<Collider2D>();

            throwingCollider.isTrigger = false;
            ObjectRigitBody.drag = 0;

            ObjectRigitBody.AddForce(throwedObject.gameObject.transform.right * mathForce);
            Destroy(throwingObjects[0]);
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
        mathForce = 125 * i;
        this.throwingObjects = throwingObjects;
    }

    public void OnTake(InputAction.CallbackContext context)
    {
        isTake = context.ReadValueAsButton();
    }
}