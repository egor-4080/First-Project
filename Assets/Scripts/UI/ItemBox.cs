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
        inventory = owner.GetComponent<Inventory>();
        this.owner = owner;
        use.interactable = item.CanUse;
        this.item = item;
        image.sprite = item._sprite;
        nameText.text = item.nameOfItem;
    }

    public void OnClick()
    {
        contextMenu.SetActive(!contextMenu.activeSelf);
    }

    public void OnUse()
    {

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
