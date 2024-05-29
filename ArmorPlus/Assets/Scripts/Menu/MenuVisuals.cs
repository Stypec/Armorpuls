using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuVisuals : MonoBehaviour
{
    [SerializeField] Currency_Handler currencyHandler;
    public static MenuVisuals singleton;
    [SerializeField] float hangarLerpDuration = 5;
    [SerializeField] Transform hangar;
    [SerializeField] TMP_Text bitsText;
    [SerializeField] GameObject addUnitButton;
    [SerializeField] GameObject unitButton;
    public int currentHangarUnitIndex;
     
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;
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
    public void EditHangarUnitIndex(int amount)
    {
        if (currentHangarUnitIndex + amount >= 0 && currentHangarUnitIndex + amount <= InventorySystem.singleton.units.Count)
        {
            currentHangarUnitIndex += amount;
            UpdateUnitIndexVisuals();
        }
    }
    public void EnableHangar()
    {
        currentHangarUnitIndex = 0;
        StopAllCoroutines();
        StartCoroutine(LerpValuesRoutine(Vector3.zero, hangarLerpDuration));
    }

    public void DisableHangar()
    {
        currentHangarUnitIndex = 0;
        StopAllCoroutines();
        hangar.position = new Vector3(hangar.position.x, -20, hangar.position.z);
    }
    public void UpdateUnitIndexVisuals()
    {
        addUnitButton.SetActive(currentHangarUnitIndex == InventorySystem.singleton.units.Count);
        unitButton.SetActive(!addUnitButton.activeSelf);
    }

    private IEnumerator LerpValuesRoutine(Vector3 destination, float duration)
    {
        float time = 0;
        UpdateUnitIndexVisuals();

        while ((destination - hangar.position).magnitude >= 0.05f)
        {
            hangar.position = Vector3.Lerp(hangar.position, destination, time / duration);

            time += Time.deltaTime;

            yield return null;
        }
        StopCoroutine(LerpValuesRoutine(destination, duration));
    }


}
