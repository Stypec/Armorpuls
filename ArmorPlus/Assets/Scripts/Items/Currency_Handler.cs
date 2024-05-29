using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Currency_Handler : MonoBehaviour
{
    private string encryptionKey = "MiklecJeStrasneSexy";

    public enum CurrencyType
    {
        Bits,
        Credit
    }
    public int bits { private set; get; }
    public int credit { private set; get; }

    public static Currency_Handler singleton;

    [SerializeField] MenuVisuals menuVisuals;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        LoadCurrencyData();
    }

    public bool EditCurrency(CurrencyType ct, int amount)
    {
        switch (ct)
        {
            case CurrencyType.Bits:
                if (bits + amount < 0)
                    return false;
                bits += amount;
                menuVisuals.UpdateBits();
                return true;

            case CurrencyType.Credit:
                if (credit + amount < 0)
                    return false;
                credit += amount;
                return true;
        }
        return true;
    }

    public void AddCurrency(CurrencyType ct, int amount)
    {
        switch (ct)
        {
            case CurrencyType.Bits:
                bits += amount;
                menuVisuals.UpdateBits();
                break;

            case CurrencyType.Credit:
                credit += amount;
                menuVisuals.UpdateBits();
                break;
        }
    }

    private void OnApplicationQuit()
    {
        SaveCurrencyData();
    }

    private void SaveCurrencyData()
    {
        CurrencyData data = new CurrencyData
        {
            bits = bits,
            credit = credit
        };

        string jsonData = JsonUtility.ToJson(data);
        string encryptedData = Encrypt(jsonData, encryptionKey);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/currencyData.json", encryptedData);
    }

    public void LoadCurrencyData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/currencyData.json"))
        {
            string encryptedData = System.IO.File.ReadAllText(Application.persistentDataPath + "/currencyData.json");
            string jsondata = Decrypt(encryptedData, encryptionKey);

            CurrencyData data = JsonUtility.FromJson<CurrencyData>(jsondata);

            bits = data.bits;
            credit = data.credit;
        }
    }

    private string Encrypt(string text, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] textBytes = Encoding.UTF8.GetBytes(text);

        using (Aes aes = Aes.Create())
        {
            using (var encryptor = aes.CreateEncryptor(keyBytes, aes.IV))
            {
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(textBytes, 0, textBytes.Length);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    private string Decrypt(string encryptedText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];

            Array.Copy(encryptedBytes, iv, iv.Length);
            Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

            using (var decryptor = aes.CreateDecryptor(keyBytes, iv))
            {
                using (var ms = new MemoryStream(cipherText))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[cipherText.Length];
                        int decryptedCount = cs.Read(plainText, 0, plainText.Length);
                        return Encoding.UTF8.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class CurrencyData
{
        public int bits;
        public int credit;
}