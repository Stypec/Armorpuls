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


[CreateAssetMenu(fileName = "Miklec ma velky Nastroj", menuName = "Inventory/Weapon Item Object", order = 0)]
public class Weapon_ItemObject : ItemObject
{
    void Awake()
    {
        itemType = ItemType.Guns;
        itemName = "Gun";
    }

    public int damage;
    public float fireRate;
    public int armorPenetration; // Miklec mi moze hocikedy prepenetrovat moju obranu <3
    public bool projectile;
    [Tooltip("Only functions when projectile is on. If it's off, you can leave it empty")]
    public GameObject projectilePrefab;
}

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