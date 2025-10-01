using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI openCloseText;
    [SerializeField] private Image crosshair;
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
        moneyText.text = text + " $";
    }

    public void ToggleOpenCloseText(bool isOpen)
    {
        if (isOpen)
        {
            openCloseText.text = "OPEN";
        }
        else
        {
            openCloseText.text = "CLOSE";
        }

    }

    public void ChangeCrosshair(Color color)
    {
        crosshair.color = color;
    }
}
