using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public ItemType itemType;
    public int itemBuyCost;
    public int itemSellCost;
}
