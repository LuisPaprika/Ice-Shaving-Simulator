using System;
using System.Collections;
using UnityEngine;

public class IceBlock : MonoBehaviour, IPickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    public static event Action<string> onHovered;

    public void Interact(Transform transform, PlayerMovement player)
    {
        
    }

    public void putInMachine(GameObject targetMachine)
    {
        if (targetMachine.TryGetComponent<IceShavingMachine>(out IceShavingMachine iceShavingMachine))
        {
            if (iceShavingMachine.iceSlot.childCount == 0)
            {
                iceShavingMachine.RefillIce();

                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, iceShavingMachine.iceSlot.transform.position, transform));
                transform.SetParent(iceShavingMachine.iceSlot.transform);

                transform.GetComponent<Collider>().enabled = false;
            }
        }
        else if (targetMachine.TryGetComponent<ShavingStand>(out ShavingStand shavingStand))
        {
            if (shavingStand.iceSlot.childCount == 0)
            {
                shavingStand.RefillIce();

                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, shavingStand.iceSlot.transform.position, transform));
                transform.SetParent(shavingStand.iceSlot.transform);

                transform.GetComponent<Collider>().enabled = false;
            }
        }

    }

    public void Hovered()
    {
        onHovered?.Invoke(interactPrompt);
    }

    public void Pickup(Transform objectPickupPoint)
    {
        transform.SetParent(objectPickupPoint);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.position, transform));
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
