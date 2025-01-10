using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] protected Sprite usedSprite;
    [SerializeField] public string NameOfItem { get; private set; }
    [SerializeField] private bool canUse;

    private Sprite sprite;
    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

        private set
        {
            spriteRenderer.sprite = value;
            sprite = value;
            spriteChanged?.Invoke();
        }
    }
    

    public UnityEvent spriteChanged {  get; private set; } = new UnityEvent();  
    public UnityEvent used {  get; private set; } = new UnityEvent();

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
        canUse = false;
        ChangeSprite();
        used?.Invoke();
    }

    private void ChangeSprite()
    {
        Sprite = usedSprite;
    }
}
