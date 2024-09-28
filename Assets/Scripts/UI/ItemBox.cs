using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image image;

    [SerializeField] private Button use;
    [SerializeField] private Button select;
    [SerializeField] private Button drop;

    private Item item;
    private PlayerContoller owner;
    private Inventory inventory;

    public void Init(Item item, PlayerContoller owner)
    {
        item.spriteChanged.AddListener(OnItemSpriteChanged);
        item.used.AddListener(OnItemUsed);
        inventory = owner.GetComponent<Inventory>();
        this.owner = owner;
        this.item = item;
        image.sprite = item.Sprite;
        nameText.text = item.NameOfItem;
        use.gameObject.SetActive(item.CanUse);
    }

    private void OnItemSpriteChanged()
    {
        image.sprite = item.Sprite;
    }

    private void OnItemUsed()
    {
        use.gameObject.SetActive(item.CanUse);
    }

    public void OnClick()
    {
        contextMenu.SetActive(!contextMenu.activeSelf);
    }

    public void OnUse()
    {
        Potion poison = item.gameObject.GetComponent<Potion>();
        poison.DoWhenUseMotion(owner);
    }

    public void OnSelect()
    {
        owner.SelectItem(item, this);
    }

    public void OnDrop()
    {
        owner.DropFromInventory(item);
        inventory.DeleteItem(this);
    }
}
