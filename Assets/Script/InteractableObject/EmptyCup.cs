using System;
using System.Collections;
using UnityEngine;

public class EmptyCup : MonoBehaviour, IPickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    public void Pickup(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
        transform.SetParent(objectPickupPoint.transform);
    }

    public void putInMachine(GameObject targetMachine)
    {
        if (targetMachine.TryGetComponent(out IceShavingMachine iceShavingMachine))
        {
            if (iceShavingMachine.cupSlot.childCount == 0)
            {
                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, iceShavingMachine.cupSlot.gameObject, transform));
                transform.SetParent(iceShavingMachine.cupSlot.transform);
            }
        }
        else if (targetMachine.TryGetComponent(out ShavingStand shavingStand))
        {
            if (shavingStand.CupSlot.childCount == 0)
            {
                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, shavingStand.CupSlot.gameObject, transform));
                transform.SetParent(shavingStand.CupSlot.transform);
            }
        }


    }

    public void Interact(GameObject transform, PlayerMovement player)
    {

    }

    public void Hovered()
    {
        
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
