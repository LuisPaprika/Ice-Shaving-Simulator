using UnityEngine;

public class WalletManager : MonoBehaviour
{
    [field: SerializeField] public static float Money { get; private set; }

    public static void SetMoney(float amount)
    {
        Money = amount;
        UIManager.Instance.SetMoneyText(Money.ToString());
    }
}
