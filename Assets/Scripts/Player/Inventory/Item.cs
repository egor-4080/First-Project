using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public Sprite _sprite { get; private set; }
    [SerializeField] public string nameOfItem {  get; private set; }
    [SerializeField] private bool canUse;

    private SpriteRenderer spriteRenderer;

    public bool CanUse => canUse;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _sprite = spriteRenderer.sprite;
        nameOfItem = gameObject.name;
    }

    public void ChangeSprite()
    {
        _sprite = spriteRenderer.sprite;
    }
}
