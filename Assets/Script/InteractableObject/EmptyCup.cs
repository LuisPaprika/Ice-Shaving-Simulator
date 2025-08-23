using System;
using System.Collections;
using UnityEngine;

public class EmptyCup : MonoBehaviour, IPickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    public static event Action<string> onHovered;
    public void Pickup(Transform objectPickupPoint)
    {
        transform.SetParent(objectPickupPoint);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.position, transform));
    }

    public void putInMachine(GameObject targetMachine)
    {
        IceShavingMachine iceShavingMachine = targetMachine.GetComponent<IceShavingMachine>();
        if (iceShavingMachine.cupSlot.childCount == 0)
        {
            transform.rotation = Quaternion.identity;
            StartCoroutine(lerpObject(transform.position, iceShavingMachine.cupSlot.transform.position, transform));
            transform.SetParent(iceShavingMachine.cupSlot.transform);
        }

    }

    public void Interact(Transform objectPickupPoint)
    {

    }

    public void Hovered()
    {
        onHovered?.Invoke(interactPrompt);
    }

    private IEnumerator lerpObject(Vector3 startPostion, Vector3 targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion;
    }
}
