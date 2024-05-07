using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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