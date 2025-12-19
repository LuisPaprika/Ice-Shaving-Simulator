using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [field: SerializeField] public static ShopManager Instance { get; private set; }
    [field: SerializeField] public int Price { get; private set; } = 0;
    [SerializeField] private TextMeshProUGUI priceUI;
    private Dictionary<ProductSO, int> boughtProducts;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void UpdatePrice(int amount)
    {
        Price += amount;
        priceUI.text = Price.ToString();
    }

    public void Purchase()
    {
        if(WalletManager.Money >= Price)
        {
            
        }

        else
        {
            UIManager.Instance.ShowAnouncingText("I don't have enough money");
        }
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    public void RemoveBoughtProduct(string targetName)
    {
        if()
    }

}
