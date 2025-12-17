using System;
using System.Collections;
using UnityEngine;

public class CupStack : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private GameObject emptyCupPrefab;
    private int currentCup = 5;

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        pickupCup(objectPickupPoint);
    }

    private void pickupCup(GameObject objectPickupPoint)
    {
        if (currentCup > 0)
        {
            GameObject emptyCup = Instantiate(emptyCupPrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
            StartCoroutine(lerpObject(transform.position, objectPickupPoint, emptyCup.transform));
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
        
        if (currentCup <= 0)
        {
            Destroy(gameObject);
        }
    }
}
