using System;
using System.Collections;
using UnityEngine;

public class IceStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject iceBlockPrefab;
    [SerializeField] private string interactPrompt;
    public static event Action<string> onHovered;

    public void Hovered()
    {
        onHovered?.Invoke(interactPrompt);
    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
    {
        getIce(objectPickupPoint);
    }

    private void getIce(Transform objectPickupPoint)
    {
        GameObject iceBlock = Instantiate(iceBlockPrefab, transform.position, Quaternion.identity, objectPickupPoint);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.position, iceBlock.transform));
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
