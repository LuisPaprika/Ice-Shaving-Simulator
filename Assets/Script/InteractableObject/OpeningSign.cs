using System;
using System.Collections;
using UnityEngine;

public class OpeningSign : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    public static event Action onOpenStore;
    public static event Action onCloseStore;

    public void Hovered()
    {

    }

    public void Interact(Transform objectPickupPoint)
    {
        flipSign(objectPickupPoint);
    }

    private void flipSign(Transform objectPickupPoint)
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            StartCoroutine(rotate(transform.rotation, targetRotation, transform));
            //onOpenStore?.Invoke();
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180, transform.rotation.z);
            StartCoroutine(rotate(transform.rotation, targetRotation, transform));
            //onCloseStore?.Invoke();
        }
    }

    private IEnumerator rotate(Quaternion startRotation, Quaternion endRotation, Transform obj)
    {
        float duration = 0.1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            obj.rotation = Quaternion.Lerp(obj.rotation, endRotation, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.rotation = endRotation;
    }
}
