using System.Collections.Generic;
using UnityEngine;

public class FireShot : Weapon
{
    [SerializeField] private ContactFilter2D filter;

    private List<Collider2D> list = new();
    private Dictionary<string, object> damageData = new();
    private Collider2D collider;

    protected override void Awake()
    {
        base.Awake();

        damageData["damage"] = damage;
        damageData["isHuman"] = true;
        collider = GetComponentInChildren<Collider2D>();
    }
    
    protected override void Fire(bool isFacing)
    {
        collider.Overlap(filter, list);
        list.ForEach(enemy => enemy.SendMessageUpwards("TakeDamage", damageData, SendMessageOptions.DontRequireReceiver));
    }
}