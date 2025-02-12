using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float price;

    public GameObject GetPrefab => prefab;
    public float GetPrice => price;
}