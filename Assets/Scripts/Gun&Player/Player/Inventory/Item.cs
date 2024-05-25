using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public Sprite _sprite { get; private set; }
    [SerializeField] public string nameOfItem {  get; private set; }
    [SerializeField] private bool canUse;

    public bool CanUse => canUse;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
        nameOfItem = gameObject.name;
    }
}
