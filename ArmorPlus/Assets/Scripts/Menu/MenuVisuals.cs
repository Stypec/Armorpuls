using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuVisuals : MonoBehaviour
{
    [SerializeField] Currency_Handler currencyHandler;
    [SerializeField] float hangarLerpDuration = 5;
    [SerializeField] Transform hangar;
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
    public void EnableHangar()
    {
        StopAllCoroutines();
        StartCoroutine(LerpValuesRoutine(Vector3.zero, hangarLerpDuration));
    }

    public void DisableHangar()
    {
        StopAllCoroutines();
        hangar.position = new Vector3(hangar.position.x, -20, hangar.position.z);
    }

    private IEnumerator LerpValuesRoutine(Vector3 destination, float duration)
    {
        float time = 0;

        while ((destination - hangar.position).magnitude >= 0.05f)
        {
            hangar.position = Vector3.Lerp(hangar.position, destination, time / duration);

            time += Time.deltaTime;

            yield return null;
        }
        StopCoroutine(LerpValuesRoutine(destination, duration));
    }
}
