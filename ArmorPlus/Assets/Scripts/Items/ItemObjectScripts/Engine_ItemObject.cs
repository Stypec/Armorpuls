using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Miklec ma velky Motor", menuName = "Inventory/Engine Item Object", order = 0)]
public class Engine_ItemObject : ItemObject
{
    void Awake()
    {
        itemType = ItemType.Motor;
        itemName = "Motor";
    }
    public int speedMultiplier;
    public float weaponPenetrationMultiplier;
}