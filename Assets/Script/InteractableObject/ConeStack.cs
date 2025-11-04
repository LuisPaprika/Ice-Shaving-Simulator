using System.Collections;
using UnityEngine;

public class ConeStack : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject conePrefab;
    private int currentCone = 5;
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

    private void pickupCone(GameObject objectPickupPoint)
    {
        if (currentCone > 0)
        {
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
