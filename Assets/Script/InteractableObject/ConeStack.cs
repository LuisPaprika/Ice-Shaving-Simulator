using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ConeStack : Pickable, IInteractable
{
    [SerializeField] private GameObject conePrefab;
    [field: SerializeField] public int maxCone {get; private set;} = 5;
    [field: SerializeField] public int currentCone {get; private set;}
    [SerializeField] private TextMeshProUGUI coneCountUI;
    void Awake()
    {
        currentCone = maxCone;
        coneCountUI.text = currentCone.ToString();
    }
    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        pickupCone(objectPickupPoint);
    }

    public void AddToStack()
    {
        currentCone++;
        coneCountUI.text = currentCone.ToString();
    }

    private void pickupCone(GameObject objectPickupPoint)
    {
        if (currentCone > 0)
        {
            currentCone--;
            coneCountUI.text = currentCone.ToString();
            GameObject emptyCup = Instantiate(conePrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
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
        
        if (currentCone <= 0)
        {
            Destroy(gameObject);
        }
    }
}
