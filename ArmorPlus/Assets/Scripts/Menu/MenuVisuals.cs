using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuVisuals : MonoBehaviour
{
    [SerializeField] Currency_Handler currencyHandler;

    [SerializeField] TMP_Text bitsText;   
     
    private void Awake()
    {
        bitsText.text = currencyHandler.bits.ToString();
    }
    public void UpdateBits()
    {
        bitsText.text = currencyHandler.bits.ToString();
    }

    public void BuyWithBits(int bits)
    {
        currencyHandler.EditCurrency(Currency_Handler.CurrencyType.Bits, bits);
    }

    public void BuyBits(int bits)
    {
        currencyHandler.AddCurrency(Currency_Handler.CurrencyType.Bits, bits);
    }

    public void BuyWithCredit(int credit)
    {
        currencyHandler.EditCurrency(Currency_Handler.CurrencyType.Credit, credit);
    }

    public void BuyCredit(int credit)
    {
        currencyHandler.AddCurrency(Currency_Handler.CurrencyType.Credit, credit);
    }
}
