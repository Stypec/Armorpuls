using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreItemValues : MonoBehaviour
{
    [SerializeField] TMP_Text price;
    [SerializeField] public Image sprite;
    public ItemObject item;
    public int amount;
    public void Visualize()
    {
        price.text = amount.ToString();
        if (sprite.sprite == null)
            sprite.color = new Color(0, 0, 0, 0);
    }
}
