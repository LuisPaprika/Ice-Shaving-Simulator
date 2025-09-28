using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI OpenCloseText;
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
        MoneyText.text = text + " $";
    }

    public void ToggleOpenCloseText(bool isOpen)
    {
        if (isOpen)
        {
            OpenCloseText.text = "OPEN";
        }
        else
        {
            OpenCloseText.text = "CLOSE";
        }
        
    }
}
