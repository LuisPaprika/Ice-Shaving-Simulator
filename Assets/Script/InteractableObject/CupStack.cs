using System;
using System.Collections;
using UnityEngine;

public class CupStack : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactPrompt;
    public static event Action<string> onHovered;
    [SerializeField] private GameObject emptyCupPrefab;
    private int currentCup = 5;

    public void Hovered()
    {
        onHovered?.Invoke(interactPrompt);
    }

    public void Interact(Transform objectPickupPoint)
    {
        pickupCup(objectPickupPoint);
    }

    private void pickupCup(Transform objectPickupPoint)
    {
        if (currentCup > 0)
        {
            //currentCup--;
            GameObject emptyCup = Instantiate(emptyCupPrefab, transform.position, Quaternion.identity, objectPickupPoint);
            StartCoroutine(lerpObject(transform.position, objectPickupPoint.position, emptyCup.transform));
        }

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
        
        if (currentCup <= 0)
        {
            Destroy(gameObject);
        }
    }
}
