using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class ThrowAndTake : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] protected List<GameObject> throwingObjects;
    [SerializeField] private AudioSource noAmmoSound;

    [SerializeField] protected float damage;
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
            StartCoroutine(SpawnWithRate());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isTake)
        {
            if (throwingObjects.Count != 5)
            {
                collision.isTrigger = false;
                collision.transform.position = new Vector3(1000, 1000, 0);
                throwingObjects.Add(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damage);
        }
        if (throwingCollider != null)
        {
            throwingCollider.isTrigger = true;
        }
    }

    private IEnumerator SpawnWithRate()
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

    public void SetBooleanValues(bool throwAction, int i)
    {
        mathForce = 125 * i;
        this.throwAction = throwAction;
    }

    private void OnTake(InputAction.CallbackContext context)
    {
        isTake = context.ReadValueAsButton();
    }
}