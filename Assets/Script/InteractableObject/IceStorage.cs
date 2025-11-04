using System;
using System.Collections;
using UnityEngine;

public class IceStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject iceBlockPrefab;
    [SerializeField] private string interactPrompt;

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        getIce(objectPickupPoint);
    }

    private void getIce(GameObject objectPickupPoint)
    {
        GameObject iceBlock = Instantiate(iceBlockPrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.transform.position, iceBlock.transform));
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
    }
}
