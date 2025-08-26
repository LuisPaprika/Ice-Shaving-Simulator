using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void Hovered()
    {

    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
    {

    }

    public void throwAwayItem(Transform objectPickupPoint)
    {
        StartCoroutine(lerpObject(objectPickupPoint.position, transform.position, objectPickupPoint.GetChild(0)));
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
        Destroy(obj.gameObject);
    }

}
