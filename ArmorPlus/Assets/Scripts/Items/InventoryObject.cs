using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Default,
    Guns,
    Motor,
    Armor
}
public class InventoryObject
{
    public ItemType type;
    public List<InventorySlot> container = new List<InventorySlot>();

    public InventoryObject(ItemType inventoryItemType)
    {
        type = inventoryItemType;
    }

    public bool HasItem(ItemObject _item)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
                return true;
        }
        return false;
    }
    public void AddItem(ItemObject _item, int _amount)
    {
        if (_item.itemType != type)
            return;
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
           container.Add(new InventorySlot(_item, _amount)); 
        }
    }

    public bool RemoveItem(ItemObject _item, int _amount)
    {
        if (_item.itemType != type)
            return false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                if (container[i].amount < _amount)
                    return false;
                container[i].RemoveAmount(_amount);
                if (container[i].amount <= 0)
                {
                    container.RemoveAt(i);
                }
                break;
            }
        }
        return true;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
