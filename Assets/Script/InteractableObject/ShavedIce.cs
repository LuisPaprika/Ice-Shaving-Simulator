using System;
using System.Collections;
using UnityEngine;

public enum ShavedIceFlavor
{
    Blank,
    Strawberry,
    Chocolate,
    Milk
}


public class ShavedIce : MonoBehaviour, IPickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    public static event Action<string> onHovered;
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
        targetPerson.GetComponent<Customer>().deliver(this);
        StartCoroutine(giveObject(transform.position, targetPerson.transform.position, transform));
    }

    public void Pickup(Transform objectPickupPoint)
    {
        transform.SetParent(objectPickupPoint);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.position, transform));
    }

    public void Hovered()
    {
        onHovered?.Invoke(interactPrompt);
    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
    {

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

    private IEnumerator giveObject(Vector3 startPostion, Vector3 targetPostion, Transform obj)
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