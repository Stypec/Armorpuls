using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public ItemObject[] itemsToSell;


    public void BuyItem(ItemObject item, int amount)
    {
        if (Currency_Handler.singleton.EditCurrency(Currency_Handler.CurrencyType.Bits, -item.itemBuyCost))
            Debug.LogError("Broke ass nigger! HAHAHAHAHAHAHAHAHAAH LLLLLLLLL"); // <-- Miklec message
        else
            InventorySystem.singleton.inventories[item.itemType].AddItem(item, amount);
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
