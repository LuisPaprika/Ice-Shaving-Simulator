using System;
using System.Collections;
using UnityEngine;


public class ShavedIce : MonoBehaviour, IPickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [field: SerializeField] public ShavedIceFlavor flavor { get; private set; }

    public void AddFlavor(ShavedIceFlavor flavor)
    {
        switch (flavor)
        {
            case ShavedIceFlavor.Strawberry:
                Instantiate(shaveIcedAsset.getPrefab(ShavedIceFlavor.Strawberry), transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case ShavedIceFlavor.Chocolate:
                Instantiate(shaveIcedAsset.getPrefab(ShavedIceFlavor.Chocolate), transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case ShavedIceFlavor.Milk:
                Instantiate(shaveIcedAsset.getPrefab(ShavedIceFlavor.Milk), transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            
            default:
                break;
        }
    }

    public void Give(GameObject targetPerson)
    {
        targetPerson.GetComponent<Customer>().Deliver(gameObject);
        StartCoroutine(giveObject(transform.position, targetPerson.gameObject, transform));
    }

    public void Pickup(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
        transform.SetParent(objectPickupPoint.transform);
    }

    public void Hovered()
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