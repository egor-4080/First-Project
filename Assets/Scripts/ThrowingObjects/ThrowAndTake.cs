using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class ThrowAndTake : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] protected List<GameObject> throwingObjects;
    [SerializeField] private AudioSource noAmmoSound;

    [SerializeField] private float throwRate;

    protected WaitForSeconds wait;

    private Rigidbody2D ObjectRigitBody;
    protected Collider2D throwingCollider;
    private GameObject throwedObject;

    private bool isThrow = true;
    private bool throwAction;
    protected bool isTake;
    private int mathForce;

    void Start()
    {
        wait = new WaitForSeconds(0.1f);
    }

    private void FixedUpdate()
    {
        if (isThrow && throwAction)
        {
            StartCoroutines();
        }
    }

    public void StartCoroutines()
    {
        StartCoroutine(SpawnWithRate());
    }

    public IEnumerator SpawnWithRate()
    {
        if (throwingObjects.Count != 0)
        {
            throwedObject = Instantiate(throwingObjects[0], throwingTransformPosition.position, throwingTransformPosition.rotation);
            ObjectRigitBody = throwedObject.GetComponent<Rigidbody2D>();
            throwingCollider = throwedObject.GetComponent<Collider2D>();

            throwingCollider.isTrigger = false;
            ObjectRigitBody.drag = 0;

            ObjectRigitBody.AddForce(throwedObject.gameObject.transform.right * mathForce);
            Destroy(throwingObjects[0]);
            throwingObjects.RemoveAt(0);

            StartCoroutine(GetMaxDrag());

            isThrow = false;
            yield return new WaitForSeconds(throwRate);
            isThrow = true;
        }
        else
        {
            noAmmoSound.Play();
            yield return new WaitForSeconds(throwRate);
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

    public void SetValues(bool throwAction, int i, List<GameObject> throwingObjects)
    {
        mathForce = 125 * i;
        this.throwingObjects = throwingObjects;
        this.throwAction = throwAction;
    }

    public void OnTake(InputAction.CallbackContext context)
    {
        isTake = context.ReadValueAsButton();
    }
}