using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUnit : MonoBehaviour
{
    public InventoryObject units;

    private void Awake()
    {
        units = new InventoryObject();
    }
}
