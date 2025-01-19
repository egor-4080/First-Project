using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class HealExploisonController : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float healBoost;

    private void Start()
    {
        GetEffectArround();
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
    
    private void GetEffectArround()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius);
        objects.ToList()
            .Select(collider => collider.GetComponent<Health>())
            .Where(health => health != null)
            .ToList().ForEach(health => health.HealCharacter(healBoost));
    }
}
