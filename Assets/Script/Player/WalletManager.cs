using UnityEngine;

public class WalletManager : MonoBehaviour
{
    [field: SerializeField] public static WalletManager Instance { get; private set; }
    [field: SerializeField] public float Money { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }
    public void SetMoney(float amount)
    {
        Money = amount;
        UIManager.Instance.SetMoneyText(Money.ToString());
    }
}
