using TMPro;
using UnityEngine;

public class CanvasDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string message)
    {
        text.text = message;
    }

    public void DisplayText()
    {
        text.enabled = true;
    }

    public void HideText()
    {
        text.enabled = false;
    }
}