using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private List<ItemData> itemDatas;

    private void Start()
    {
        itemDatas = FindObjectsByType<ItemData>(FindObjectsSortMode.None).ToList();
    }
}