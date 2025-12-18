using System;
using System.Collections;
using UnityEngine;


public class ShavedIce : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [field: SerializeField] public ShavedIceFlavor flavor { get; private set; }

    public void AddFlavor(ShavedIceFlavor flavor)
    {
        Instantiate(shaveIcedAsset.getPrefab(flavor), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Give(GameObject targetPerson)
    {
        targetPerson.GetComponent<Customer>().GetDelivered(gameObject);
        StartCoroutine(giveObject(transform.position, targetPerson.gameObject, transform));
    }

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

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
    }

    private IEnumerator giveObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
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
        Destroy(obj.gameObject);
    }
}