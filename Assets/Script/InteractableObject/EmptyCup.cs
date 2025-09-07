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
        if (targetMachine.TryGetComponent<IceShavingMachine>(out IceShavingMachine iceShavingMachine))
        {
            if (iceShavingMachine.cupSlot.childCount == 0)
            {
                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, iceShavingMachine.cupSlot.transform.position, transform));
                transform.SetParent(iceShavingMachine.cupSlot.transform);
            }
        }
        else if (targetMachine.TryGetComponent<ShavingStand>(out ShavingStand shavingStand))
        {
            if (shavingStand.CupSlot.childCount == 0)
            {
                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, shavingStand.CupSlot.transform.position, transform));
                transform.SetParent(shavingStand.CupSlot.transform);
            }
        }


    }

    public void Interact(Transform transform, PlayerMovement player)
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
