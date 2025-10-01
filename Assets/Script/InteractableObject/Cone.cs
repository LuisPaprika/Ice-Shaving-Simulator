using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour, IPickable, IInteractable
{
    [field: SerializeField] public int currentScoop { get; private set; } = 0;
    [SerializeField] private GameObject[] scoopPos = new GameObject[3];

    [SerializeField] private int chocCount = 0;
    [SerializeField] private int vanilCount = 0;
    [SerializeField] private int strawCount = 0;

    [field: SerializeField]
    public Dictionary<IceCreamFlavor, int> flavors { get; private set; }
        = new Dictionary<IceCreamFlavor, int>
    {
        { IceCreamFlavor.Chocolate, 0 },
        { IceCreamFlavor.Vanilla, 0 },
        { IceCreamFlavor.Strawberry, 0 }
    };

    void Awake()
    {
        flavors[IceCreamFlavor.Chocolate] = chocCount;
        flavors[IceCreamFlavor.Vanilla] = vanilCount;
        flavors[IceCreamFlavor.Strawberry] = strawCount;
    }

    public void Pickup(Transform objectPickupPoint)
    {
        transform.SetParent(objectPickupPoint);
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
    }

    public void AddScoop(GameObject scoop, IceCreamFlavor flavor)
    {
        scoop.transform.SetParent(scoopPos[currentScoop].transform);
        StartCoroutine(lerpObject(scoop.transform.position, scoopPos[currentScoop].transform.gameObject, scoop.transform));

        currentScoop++;
        flavors[flavor]++;

    }

    public void Give(GameObject targetPerson)
    {
        targetPerson.GetComponent<Customer>().Deliver(gameObject);
        StartCoroutine(giveObject(transform.position, targetPerson.gameObject, transform));
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

    public void Hovered()
    {

    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
    {

    }
}
