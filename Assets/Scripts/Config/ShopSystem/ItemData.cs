using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float price;

    public GameObject GetPrefab => prefab;
    public Weapon GetWeapon => weapon;
    public float GetPrice => price;
}