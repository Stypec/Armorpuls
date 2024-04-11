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
public abstract class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
}
