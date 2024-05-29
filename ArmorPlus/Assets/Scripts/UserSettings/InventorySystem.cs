using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class InventorySystem : MonoBehaviour
{
    public Dictionary<ItemType, InventoryObject> inventories = new Dictionary<ItemType, InventoryObject>();
    public static InventorySystem singleton;
    public List<Unit> units;
    public int maxUnitCount;

    public Weapon_ItemObject defaultWeapon;
    public Engine_ItemObject defaultEngine;
    public Armor_ItemObject defaultArmor;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);

        /// <READ>
        /// Edit it so it works with data saving
        /// <READ>
        inventories = new Dictionary<ItemType, InventoryObject>();
        for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            ItemType t = (ItemType)i;
            inventories.Add(t, new InventoryObject(t));
        };
    }

    public void AddUnit()
    {
        units.Add(new Unit(15000, 3.5f, 150, defaultWeapon, defaultEngine, defaultArmor));
        MenuVisuals.singleton.UpdateUnitIndexVisuals();
    }

}


