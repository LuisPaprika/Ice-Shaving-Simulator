using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CupStack : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private GameObject emptyCupPrefab;
    [field: SerializeField] public int maxCup {get; private set;} = 5;
    [field: SerializeField] public int currentCup {get; private set;}
    [SerializeField] private TextMeshProUGUI cupTextUI;
    void Awake()
    {
        currentCup = maxCup;
        cupTextUI.text = currentCup.ToString();
    }

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
            currentCup--;
            cupTextUI.text = currentCup.ToString();
            GameObject emptyCup = Instantiate(emptyCupPrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
            StartCoroutine(lerpObject(transform.position, objectPickupPoint, emptyCup.transform));
        }

    }

    public void AddToStack()
    {
        currentCup++;
        cupTextUI.text = currentCup.ToString();
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
