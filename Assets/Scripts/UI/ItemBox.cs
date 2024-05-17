using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image image;

    private Item item;

    public void Init(Item item)
    {
        this.item = item;
        image.sprite = item._sprite;
        nameText.text = item.nameOfItem;
    }
}
