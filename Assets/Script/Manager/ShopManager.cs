using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [field: SerializeField] public static ShopManager Instance { get; private set; }
    [field: SerializeField] public int Price { get; private set; } = 0;
    public static event Action onPurchase;
    [field: SerializeField] public List<Order> cart { get; private set; } = new List<Order>();
    [SerializeField] private ProductBox spawnBox;

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

        ProductPanel.onDecreaseProductAmount += RemoveBoughtProduct;
        ProductPanel.onIncreaseProductAmount += AddBoughtProduct;

        DayManager.onDaySet += ClearCart;
    }

    private void UpdatePrice(int amount)
    {
        Price += amount;
        UIManager.Instance.SetPriceText(Price.ToString());
    }

    public void Purchase()
    {
        if (WalletManager.Instance.Money >= Price)
        {
            foreach(Order order in cart)
            {
                spawnBox.AddProducts(order);
            }

            WalletManager.Instance.SetMoney(WalletManager.Instance.Money - Price);
            cart.Clear();
            onPurchase?.Invoke();
            Price = 0;
            UIManager.Instance.SetPriceText(Price.ToString());
        }

        else
        {
            StartCoroutine(UIManager.Instance.ShowAnouncingText("I don't have enough money"));
        }
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    private void ClearCart()
    {
        cart.Clear();
    }

    private void AddBoughtProduct(ProductSO productSO)
    {
        if (cart.Count == 0)
        {
            cart.Add(new Order(productSO, 1));
            UpdatePrice(productSO.price);
            return;
        }

        foreach (Order order in cart)
        {
            if (order.product == productSO)
            {
                order.amount++;
                UpdatePrice(productSO.price);
                return;
            }
        }

        cart.Add(new Order(productSO, 1));
        UpdatePrice(productSO.price);
        return;
    }

    private void RemoveBoughtProduct(ProductSO productSO)
    {
        if (cart.Count() == 0)
        {
            return;
        }

        foreach (Order order in cart)
        {
            if (order.product == productSO)
            {
                order.amount--;
                UpdatePrice(-1 * productSO.price);
                if (order.amount <= 0)
                {
                    cart.Remove(order);
                }
                return;
            }
        }
    }

}
