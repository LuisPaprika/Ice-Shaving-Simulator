using TMPro;
using UnityEngine;

public class CanvasDisplay : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public void EnablingCanvas(bool value)
    {
        canvas.SetActive(value);
    }
}