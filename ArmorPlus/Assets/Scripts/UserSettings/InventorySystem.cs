using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public Dictionary<ItemType, InventoryObject> inventories;
    public static InventorySystem singleton;
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;


        /// <READ>
        /// Edit it so it works with data saving
        /// <READ>
        for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            ItemType t = (ItemType)i;
            inventories.Add(t, new InventoryObject(t));
        };
    }

    public void AddItemToInventory(ItemObject item, int amount)
    {
        inventories[item.itemType].AddItem(item, amount);
    }
    public void RemoveItemFromInventory(ItemObject item, int amount)
    {
        inventories[item.itemType].RemoveItem(item, amount);
    }

}
