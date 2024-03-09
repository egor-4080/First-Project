using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class BaseComponent : ThrowAndTake
{
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (isTake)
        {
            if (throwingObjects.Count != 5 && TryGetComponent(out BaseComponent isThrow))
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
    }*/
}
