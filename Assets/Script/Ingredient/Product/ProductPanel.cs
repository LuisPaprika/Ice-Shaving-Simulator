using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] private ProductSO productSO;

    [SerializeField] private TextMeshProUGUI amountUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    private int amount = 0;

    void Awake()
    {
        nameUI.text = productSO.productName;

        DayManager.onDaySet += ResetAmount;
    }
    public void addAmount()
    {
        amount++;
        amountUI.text = amount.ToString();
        ShopManager.Instance.UpdatePrice(productSO.price);
    }

    public void removeAmount()
    {
        if(amount > 0)
        {
            amount--;
            amountUI.text = amount.ToString();
            ShopManager.Instance.UpdatePrice(-1 * productSO.price);
        }
    }

    private void ResetAmount(int temp)
    {
        amount = 0;
    }

}
