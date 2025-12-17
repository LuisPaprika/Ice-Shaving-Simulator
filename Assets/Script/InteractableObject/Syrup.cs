using System;
using System.Collections;
using UnityEngine;

public class Syrup : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private ShavedIceFlavor flavor;

    public void Interact(GameObject transform, PlayerMovement player)
    {

    }
    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public ShavedIceFlavor getFlavor()
    {
        return flavor;
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
