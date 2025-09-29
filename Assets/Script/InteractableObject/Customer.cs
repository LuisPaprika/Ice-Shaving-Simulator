using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [field: SerializeField] public bool Interactable;
    private float waitingTime;
    private ShavedIceFlavor req;
    [SerializeField] private GameObject timeSlider;
    [SerializeField] private Transform requestSlot;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [SerializeField] private ParticleSystem correctParticle;
    [SerializeField] private ParticleSystem wrongParticle;


    public void Deliver(ShavedIce recievedItem)
    {
        if (recievedItem.flavor == req)
        {
            Interactable = false;
            Instantiate(correctParticle, transform.position, Quaternion.identity);
            Exit();
            WalletManager.SetMoney(WalletManager.Money + 50);
        }
        else
        {
            Interactable = false;
            Instantiate(wrongParticle, transform.position, Quaternion.identity);
            Exit();
        }
    }

    public void StartWaiting()
    {
        Interactable = true;
        StartCoroutine(Waiting(waitingTime));
    }

    public void Init(ShavedIceFlavor request, float waitTime, Transform position)
    {
        Interactable = false;
        waitingTime = waitTime;
        req = request;

        gameObject.GetComponent<NavMeshAgent>().SetDestination(position.position);
    }

    private IEnumerator Waiting(float time)
    {
        timeSlider.SetActive(true);
        GameObject requestDisplay = Instantiate(shaveIcedAsset.getPrefab(req), requestSlot.position, Quaternion.identity, requestSlot);
        requestDisplay.GetComponent<Collider>().enabled = false;

        timeSlider.GetComponent<Slider>().maxValue = time;
        float currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeSlider.GetComponent<Slider>().value = currentTime;
            yield return null;
        }

        Interactable = false;

        Instantiate(wrongParticle, transform.position, Quaternion.identity);
        Exit();
    }
    private void Exit()
    {
        QueueManager.MoveQueue();
        QueueManager.SetCustomerCount(QueueManager.customerCount - 1);

        transform.GetChild(0).gameObject.SetActive(false);
        timeSlider.SetActive(false);
        timeSlider.GetComponent<Slider>().value = 0f;
    }
}
