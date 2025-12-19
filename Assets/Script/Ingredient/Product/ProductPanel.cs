using System;
using TMPro;
using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] private ProductSO productSO;

    [SerializeField] private TextMeshProUGUI amountUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    public static event Action<ProductSO> onIncreaseProductAmount;
    public static event Action<ProductSO> onDecreaseProductAmount;
    private int amount = 0;

    void Awake()
    {
        nameUI.text = productSO.productName;

        DayManager.onDaySet += ResetAmount;
        ShopManager.onPurchase += ResetAmount;
    }
    public void addAmount()
    {
        amount++;
        amountUI.text = amount.ToString();
        onIncreaseProductAmount?.Invoke(productSO);
    }

    public void removeAmount()
    {
        if(amount > 0)
        {
            amount--;
            amountUI.text = amount.ToString();
            onDecreaseProductAmount?.Invoke(productSO);
        }
    }

    private void ResetAmount()
    {
        amount = 0;
        amountUI.text = amount.ToString();
    }

}
