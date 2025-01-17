using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FastShootExploison : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float shootBoostTime;
    
    private void Start()
    {
        GetEffectArround();
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }

    private void GetEffectArround()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in objects)
        {
            collider.SendMessageUpwards("GetFastShoot", shootBoostTime, SendMessageOptions.DontRequireReceiver);
        }
    }
}
