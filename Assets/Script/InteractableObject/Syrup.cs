using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Syrup : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [field: SerializeField] public ShavedIceFlavor flavor { get; private set; }
    [field: SerializeField] public int CurrentUsageLeft { get; private set; }
    [field: SerializeField] public int MaxUsage { get; private set; } = 10;
    [SerializeField] private TextMeshProUGUI usageCountUI;

    void Awake()
    {
        CurrentUsageLeft = MaxUsage;
        usageCountUI.text = CurrentUsageLeft.ToString();
    }
    public void Interact(GameObject transform, PlayerMovement player)
    {

    }
    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void SplashSyrup(ShavedIce shavedIce)
    {
        CurrentUsageLeft--;
        usageCountUI.text = CurrentUsageLeft.ToString();
        shavedIce.AddFlavor(flavor);

        if(CurrentUsageLeft <= 0)
        {
            Destroy(this);
        }
    }

    private IEnumerator lerpObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion.transform.position, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion.transform.position;
    }
}
