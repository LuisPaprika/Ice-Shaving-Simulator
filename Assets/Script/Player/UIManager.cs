using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private TextMeshProUGUI openCloseText;
    [SerializeField] private TextMeshProUGUI anouncingText;
    [SerializeField] private GameObject anouncingTextPanel;
    [SerializeField] private GameObject salesPanel;
    [SerializeField] private TextMeshProUGUI salesMenuPrefab;
    [SerializeField] private Image crosshair;
    [SerializeField] private GameObject shoppingPanel;
    [SerializeField] private TextMeshProUGUI cartPriceText;
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

    public IEnumerator ShowAnouncingText(string text)
    {
        anouncingTextPanel.SetActive(true);
        anouncingTextPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        yield return new WaitForSeconds(3);
        anouncingTextPanel.SetActive(false);
        anouncingTextPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    public void SetMoneyText(string text)
    {
        moneyText.text = text + " $";
    }

    public void SetDayText(string text)
    {
        dayText.text = "Day " + text;
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

    public void CreateSales()
    {
        PlayerMovement.Instance.EnablingMovement(false);
        salesPanel.SetActive(true);
        foreach(ScriptableObject so in IncomeManager.Instance.Sales)
        {
            MenuSO menuSO = so as MenuSO;
            if(menuSO != null)
            {
                if(menuSO.sales > 0)
                {
                    TextMeshProUGUI text = Instantiate(salesMenuPrefab, salesPanel.transform.GetChild(0).transform);
                    text.text = menuSO.menuName + "        " + "x" + menuSO.sales + "        = " + menuSO.sales*menuSO.price;
                }
            }
        }
    }

    public void DisableSalesResult()
    {
        PlayerMovement.Instance.EnablingMovement(true);

        foreach(Transform child in salesPanel.transform.GetChild(0).transform)
        {
            Destroy(child.gameObject);
        }

        salesPanel.SetActive(false);
    }

    public void ToggleShoppingUI()
    {
        shoppingPanel.SetActive(!shoppingPanel.activeSelf);
        salesPanel.SetActive(!salesPanel.activeSelf);
    }

    public void SetPriceText(string message)
    {
        cartPriceText.text = message;
    }
}
