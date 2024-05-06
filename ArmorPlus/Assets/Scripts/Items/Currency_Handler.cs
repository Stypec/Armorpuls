using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency_Handler : MonoBehaviour
{
    public enum CurrencyType
    {
        Bits,
        Credit
    }
    public int bits {private set; get;}
    public int credit {private set; get;}

    public static Currency_Handler singleton;


    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }

    public bool EditCurrency(CurrencyType ct, int amount)
    {
        switch (ct)
        {
            case CurrencyType.Bits:
                if (bits - amount < 0)
                    return false;
                ct += amount;
                break;

            case CurrencyType.Credit:
                if (credit - amount < 0)
                    return false;
                ct += amount;
                break;
        }
        return true;
    }

}
