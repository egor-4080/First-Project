using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObjectController : MonoBehaviour
{
    [SerializeField] private float forceSpeed;
    [SerializeField] private float damage;
    [SerializeField] private AudioSource audioSource;

    private Collider2D objectsCollider;
    private Rigidbody2D rigitBody;
    private Poison poison;
    private bool isPoison;
    protected bool isBreak;

    private void Awake()
    {
        objectsCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if(TryGetComponent(out poison))
        {
            isPoison = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigitBody.drag = 20;
        objectsCollider.isTrigger = true;
        if (isPoison)
        {
            OnBreak(poison.FindAllobjects());
        }
        if (!collision.gameObject.TryGetComponent(out PlayerContoller player))
        {
            isBreak = true;
        }
        //Damage
    }

    public void Initialization(bool isBreak)
    {
        if (this.isBreak != isBreak)
        {
            this.isBreak = true;
        }
    }

    protected void OnBreak(Collider2D[] bodiesForEffect)
    {
        damage = 0;
        foreach (var body in bodiesForEffect)
        {
            poison.DoEffectWithBody(body);
        }
    }

    public void Throw(Vector3 direction)
    {
        if (isBreak)
        {
            objectsCollider.isTrigger = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            rigitBody.velocity = direction * forceSpeed;
        }
    }
}
