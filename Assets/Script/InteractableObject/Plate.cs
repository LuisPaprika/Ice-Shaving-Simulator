using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour, IPickable
{


    public void Pickup(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
        transform.SetParent(objectPickupPoint.transform);
    }

    public void Give(GameObject targetPerson)
    {
        if (gameObject.GetComponentInChildren<Waffle>())
        {
            targetPerson.GetComponent<Customer>().GetDelivered(gameObject);
            StartCoroutine(giveObject(transform.position, targetPerson.gameObject, transform));
        }
    }

    public void Stack(GameObject targetStack)
    {
        targetStack.GetComponent<PlateStack>().AddToStack();
        StartCoroutine(stackObject(transform.position, targetStack, transform));
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

    private IEnumerator stackObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion.transform.position, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj.gameObject);
    }
}
