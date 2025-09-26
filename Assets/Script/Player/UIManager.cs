using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public static UIManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI MoneyText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetMoneyText(string text)
    {
        MoneyText.text = text;
    }
}
