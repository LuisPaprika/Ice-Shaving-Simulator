using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private TextMeshProUGUI openCloseText;
    [SerializeField] private GameObject anouncingText;
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

    public IEnumerator ShowAnouncingText(string text)
    {
        anouncingText.SetActive(true);
        anouncingText.GetComponentInChildren<TextMeshProUGUI>().text = text;
        yield return new WaitForSeconds(3);
        anouncingText.SetActive(false);
        anouncingText.GetComponentInChildren<TextMeshProUGUI>().text = "";
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
}
