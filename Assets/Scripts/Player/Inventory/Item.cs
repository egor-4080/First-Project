using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] protected Sprite usedSprite;
    [SerializeField] public Sprite Sprite
    { 
        get => Sprite;
        private set
        {
            spriteChanged?.Invoke();
            Sprite = value;
            spriteRenderer.sprite = value;
        }
    }
    [SerializeField] public string NameOfItem {  get; private set; }
    [SerializeField] private bool canUse;

    public UnityEvent spriteChanged {  get; private set; }

    private SpriteRenderer spriteRenderer;

    public bool CanUse => canUse;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite = spriteRenderer.sprite;
        NameOfItem = gameObject.name;
    }

    public void Used()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        Sprite = spriteRenderer.sprite;
    }
}
