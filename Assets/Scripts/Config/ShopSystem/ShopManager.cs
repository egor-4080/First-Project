using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private WeaponManager weaponManager;
    
    private List<ShopItem> shopItems = new();

    private void Start()
    {
        shopItems = canvas.GetComponentsInChildren<ShopItem>().ToList();
        AddMethodAsListener();
    }

    private void AddMethodAsListener()
    {
        foreach (var shopItem in shopItems)
        {
            shopItem.Button.onClick.AddListener(() => { OnButtonTapped(shopItem); });
        }
    }

    private void OnButtonTapped(ShopItem shopItem)
    {
        if (shopItem.IsBought)
        {
            weaponManager.GiveWeapon(shopItem.ItemData.GetPrefab);
        }
        else
        {
            if (CoinManager.Instanse.TryBuy(shopItem.ItemData.GetPrice))
            {
                weaponManager.GiveWeapon(shopItem.ItemData.GetPrefab);
                shopItem.OnPurchasesComplete();
            }
            else
            {
                CoinManager.Instanse.PlayAudio();
            }
        }
    }
}