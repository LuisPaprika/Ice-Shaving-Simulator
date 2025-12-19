using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] private ProductSO productSO;

    [SerializeField] private TextMeshProUGUI amountUI;
    [SerializeField] private TextMeshProUGUI nameUI;

    void Awake()
    {
        nameUI.text = productSO.productName;
    }
    public void addAmount()
    {
        productSO.amount++;
        amountUI.text = productSO.amount.ToString();
        ShopManager.Instance.UpdatePrice(productSO.price);
    }

    public void removeAmount()
    {
        if(productSO.amount > 0)
        {
            productSO.amount--;
            amountUI.text = productSO.amount.ToString();
            ShopManager.Instance.UpdatePrice(-1 * productSO.price);
        }
    }
}
