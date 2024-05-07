using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class InventorySystem : MonoBehaviour
{
    public Dictionary<ItemType, InventoryObject> inventories = new Dictionary<ItemType, InventoryObject>();
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
        inventories = new Dictionary<ItemType, InventoryObject>();
        for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            ItemType t = (ItemType)i;
            inventories.Add(t, new InventoryObject(t));
        };
    }

}


