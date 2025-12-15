using System.Collections;
using UnityEngine;

public class PlateStack : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private GameObject emptyPlatePrefab;
    [field: SerializeField] public int maxPlate {get; private set;} = 8;
    [field: SerializeField] public int currentPlate {get; private set;}
    void Awake()
    {
        currentPlate = maxPlate;
    }

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void AddToStack()
    {
        currentPlate++;
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
        if (currentPlate > 0)
        {
            currentPlate--;
            GameObject emptyCup = Instantiate(emptyPlatePrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
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
        
        if (currentPlate <= 0)
        {
            Destroy(gameObject);
        }
    }
}
