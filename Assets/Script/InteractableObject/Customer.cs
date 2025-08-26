using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private float moveSpeed;
    private float waitingTime;
    private Transform endPosition;
    private Transform waitPosition;
    private ShavedIceFlavor req;
    [SerializeField] private Transform requestSlot;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [SerializeField] private ParticleSystem correctParticle;
    [SerializeField] private ParticleSystem wrongParticle;


    public void deliver(ShavedIce recievedItem)
    {
        if (recievedItem.flavor == req)
        {
            Instantiate(correctParticle, transform.position, Quaternion.identity);
            StartCoroutine(exiting(endPosition.position, transform));
        }
        else
        {
            Instantiate(wrongParticle, transform.position, Quaternion.identity);
            StartCoroutine(exiting(endPosition.position, transform));
        }
    }

    public void Init(ShavedIceFlavor request, Transform waitPoint, Transform endPoint, float waitTime, float speed)
    {
        waitPosition = waitPoint;
        endPosition = endPoint;
        moveSpeed = speed;
        waitingTime = waitTime;
        req = request;

        StartCoroutine(goToQueue(waitPosition.position, endPoint.position, transform));
    }

    private IEnumerator goToQueue(Vector3 targetPostion, Vector3 destroyPosition, Transform obj)
    {
        while (Vector3.Distance(transform.position, targetPostion) != 0f)
        {
            obj.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        obj.position = targetPostion;

        GameObject requestDisplay = Instantiate(shaveIcedAsset.getPrefab(req), requestSlot.position, Quaternion.identity, requestSlot);
        requestDisplay.GetComponent<Collider>().enabled = false;

        StartCoroutine(waiting(waitingTime, destroyPosition));
    }

    private IEnumerator waiting(float time, Vector3 position)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(exiting(position, transform));
    }

    private IEnumerator exiting(Vector3 targetPostion, Transform obj)
    {
        disableCustomer();

        while (Vector3.Distance(transform.position, targetPostion) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }

        obj.position = targetPostion;
        Destroy(transform.gameObject);
    }

    private void disableCustomer()
    {
        transform.GetComponent<Collider>().enabled = false;
        Destroy(transform.GetChild(0).gameObject);
    }
}
