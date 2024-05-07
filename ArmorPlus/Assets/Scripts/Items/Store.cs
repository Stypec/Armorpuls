using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public void BuyItem(StoreItemValues _item)
    {
        if (!Currency_Handler.singleton.EditCurrency(Currency_Handler.CurrencyType.Bits, -_item.item.itemBuyCost))
            Debug.LogError("Broke ass nigger! HAHAHAHAHAHAHAHAHAAH LLLLLLLLL"); // <-- Miklec message
        else
            InventorySystem.singleton.inventories[_item.item.itemType].AddItem(_item.item, _item.amount);
    
        Debug.Log(InventorySystem.singleton.inventories[_item.item.itemType].container.Count);
    }
    public void BuyItem(ItemObject item, int amount)
    {
        if (!Currency_Handler.singleton.EditCurrency(Currency_Handler.CurrencyType.Bits, -item.itemBuyCost))
            Debug.LogError("Broke ass nigger! HAHAHAHAHAHAHAHAHAAH LLLLLLLLL"); // <-- Miklec message
        else
            InventorySystem.singleton.inventories[item.itemType].AddItem(item, amount);

        Debug.Log(InventorySystem.singleton.inventories[item.itemType].container.Count);
    }

    public void SellItem(StoreItemValues _item)
    {
        if (InventorySystem.singleton.inventories[_item.item.itemType].HasItem(_item.item))
        {
            if (InventorySystem.singleton.inventories[_item.item.itemType].RemoveItem(_item.item, _item.amount))
                Currency_Handler.singleton.EditCurrency(Currency_Handler.CurrencyType.Bits, _item.item.itemSellCost * _item.amount);
        }
    }
    public void SellItem(ItemObject item, int amount)
    {
        if (InventorySystem.singleton.inventories[item.itemType].HasItem(item))
        {
            if (InventorySystem.singleton.inventories[item.itemType].RemoveItem(item, amount))
                Currency_Handler.singleton.EditCurrency(Currency_Handler.CurrencyType.Bits, item.itemSellCost * amount);
        }
    }
}
