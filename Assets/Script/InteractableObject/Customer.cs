using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform endPosition;
    private Transform waitPosition;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [SerializeField] private Transform requestSlot;
    private ShavedIceFlavor request;

    public void deliver(ShavedIce recievedItem)
    {
        if (recievedItem.flavor == request)
        {
            Debug.Log("Thanks");
        }
        else
        {
            Debug.Log("No");
        }
        StartCoroutine(moveToDestroy(transform.position, endPosition.position, transform));
    }

    public void Init(Transform wPosition, Transform ePosition)
    {
        request = shaveIcedAsset.getRandomFlavor();
        waitPosition = wPosition;
        endPosition = ePosition;

        StartCoroutine(moveToWait(transform.position, waitPosition.position, transform));
    }

    private IEnumerator moveToWait(Vector3 startPostion, Vector3 targetPostion, Transform obj)
    {
        while (Vector3.Distance(transform.position, targetPostion) != 0f)
        {
            obj.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        obj.position = targetPostion;

        GameObject requestDisplay = Instantiate(shaveIcedAsset.getPrefab(request), requestSlot.position, Quaternion.identity, requestSlot);
        requestDisplay.GetComponent<Collider>().enabled = false;
    }

    private IEnumerator moveToDestroy(Vector3 startPostion, Vector3 targetPostion, Transform obj)
    {
        while (Vector3.Distance(transform.position, targetPostion) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        obj.position = targetPostion;
        Destroy(transform.gameObject);
    }
}
