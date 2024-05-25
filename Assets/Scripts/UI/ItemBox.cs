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

    public void Init(Item item)
    {
        use.interactable = item.CanUse;
        this.item = item;
        image.sprite = item._sprite;
        nameText.text = item.nameOfItem;
    }

    public void OnClick()
    {
        contextMenu.SetActive(!contextMenu.activeSelf);
    }
}
