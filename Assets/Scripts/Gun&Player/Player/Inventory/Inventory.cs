using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemBox> items = new();

    public void AddItem(ItemBox item)
    {
        items.Add(item);
    }
}
