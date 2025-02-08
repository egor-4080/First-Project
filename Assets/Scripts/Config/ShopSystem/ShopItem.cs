using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private TMP_Text priceText;
    
    public ItemData ItemData => itemData;
    public Button Button { get; private set; }
    public bool IsBought { get; private set; }

    private void Awake()
    {
        Button = GetComponentInChildren<Button>();
        priceText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPurchasesComplete()
    {
        priceText.text = "BOUGHT";
        IsBought = true;
    }
}