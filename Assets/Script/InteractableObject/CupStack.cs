using System;
using System.Collections;
using UnityEngine;

public class CupStack : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private GameObject emptyCupPrefab;
    [field: SerializeField] public int maxCup {get; private set;} = 5;
    [field: SerializeField] public int currentCup {get; private set;}
    void Awake()
    {
        currentCup = maxCup;
    }

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void Pickup(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
        transform.SetParent(objectPickupPoint.transform);
    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        pickupCup(objectPickupPoint);
    }

    private void pickupCup(GameObject objectPickupPoint)
    {
        if (currentCup > 0)
        {
            currentCup--;
            GameObject emptyCup = Instantiate(emptyCupPrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
            StartCoroutine(lerpObject(transform.position, objectPickupPoint, emptyCup.transform));
        }

    }

    public void AddToStack()
    {
        currentCup++;
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
