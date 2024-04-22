using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObjectController : MonoBehaviour
{
    [SerializeField] private float forceSpeed;

    private Collider2D objectsCollider;
    private Rigidbody2D rigitBody;

    private void Awake()
    {
        objectsCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.TryGetComponent(out PlayerContoller player))
        {

        }
        else
        {

        }
        objectsCollider.isTrigger = true;
    }

    protected void OnBreak(Collider2D[] bodiesForEffect)
    {
        foreach (var body in bodiesForEffect)
        {
            DoEffectWithBody(body);
        }
    }

    protected virtual void DoEffectWithBody(Collider2D body) { }
    public virtual void DoWhenUseMotion(Health player)
    {
        gameObject.SetActive(true);
        StartCoroutine(MusicEffect());
    }

    public void Throw(Vector3 direction)
    {
        rigitBody.velocity = direction * forceSpeed;
    }
}
