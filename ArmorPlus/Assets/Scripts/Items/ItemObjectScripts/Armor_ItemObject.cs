using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Miklec je tak Pevny~", menuName = "Inventory/Armor Item Object", order = 0)]
public class Armor_ItemObject : ItemObject
{
    void Awake()
    {
        itemType = ItemType.Armor;
        itemName = "Armor";
    }
    public float healthMultiplier;
    [Tooltip("Works as damage reduction")] public int armorValue;
}