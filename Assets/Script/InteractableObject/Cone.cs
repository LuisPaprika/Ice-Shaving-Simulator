using System.Collections;
using UnityEngine;

public class Cone : Pickable, IInteractable
{
    [field: SerializeField] public int currentScoop { get; private set; } = 0;
    [SerializeField] private GameObject[] scoopPos = new GameObject[3];

    [field: SerializeField] public int chocCount { get; private set; } = 0;
    [field: SerializeField] public int vanilCount { get; private set; } = 0;
    [field: SerializeField] public int strawCount { get; private set; } = 0;

    public void AddScoop(GameObject scoop, IceCreamFlavor flavor)
    {
        scoop.transform.SetParent(scoopPos[currentScoop].transform);
        StartCoroutine(lerpObject(scoop.transform.position, scoopPos[currentScoop].transform.gameObject, scoop.transform));

        currentScoop++;
        switch (flavor)
        {
            case IceCreamFlavor.Vanilla:
                vanilCount++;
                return;
            case IceCreamFlavor.Chocolate:
                chocCount++;
                return;
            case IceCreamFlavor.Strawberry:
                strawCount++;
                return;

            default:
                return;
        }
    }

    public void Give(GameObject targetPerson)
    {
        targetPerson.GetComponent<Customer>().GetDelivered(gameObject);
        StartCoroutine(giveObject(transform.position, targetPerson.gameObject, transform));
    }

    private IEnumerator lerpObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(obj.position, targetPostion.transform.position, currentTime / duration);
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

    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

    }
}
