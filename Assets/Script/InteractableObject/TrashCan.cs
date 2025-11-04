using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

    }

    public void throwAwayItem(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(objectPickupPoint.transform.position, transform.position, objectPickupPoint.transform.GetChild(0)));
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
