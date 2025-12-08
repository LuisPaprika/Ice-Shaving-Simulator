using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [field: SerializeField] public bool Interactable;
    private float waitingTime;
    public GameObject request;
    [SerializeField] private GameObject timeSlider;
    [SerializeField] private Transform requestSlot;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [SerializeField] private ParticleSystem correctParticle;
    [SerializeField] private ParticleSystem wrongParticle;


    public void Deliver(GameObject recievedItem)
    {
        if (request.GetComponent<ShavedIce>())
        {
            if (DeliverManager.CompareShavedIce(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);
                WalletManager.SetMoney(WalletManager.Money + 50);
                Exit();
            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
                Exit();
            }
        }
        else if (request.GetComponent<Cone>())
        {
            if (DeliverManager.CompareCone(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);
                WalletManager.SetMoney(WalletManager.Money + 50);
                Exit();
            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
                Exit();
            }
        }
        else if (request.GetComponent<Plate>())
        {
            if (DeliverManager.ComparePlate(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);
                WalletManager.SetMoney(WalletManager.Money + 50);
                Exit();
            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
                Exit();
            }
        }
    }

    public void StartWaiting()
    {
        StartCoroutine(Waiting(waitingTime));
    }

    public void Init(GameObject request, float waitTime, Transform position)
    {
        Interactable = false;
        waitingTime = waitTime;
        this.request = request;

        gameObject.GetComponent<NavMeshAgent>().SetDestination(position.position);
    }

    private IEnumerator Waiting(float time)
    {
        Interactable = true;

        timeSlider.SetActive(true);
        GameObject requestDisplay = Instantiate(request, requestSlot.position, Quaternion.identity, requestSlot);
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

        transform.GetChild(0).gameObject.SetActive(false);
        timeSlider.SetActive(false);
        timeSlider.GetComponent<Slider>().value = 0f;
    }

}
