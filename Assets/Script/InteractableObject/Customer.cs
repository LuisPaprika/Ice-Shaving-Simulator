using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [field: SerializeField] public bool Interactable;
    private bool isLeaving;
    private float waitingTime;
    public GameObject request;
    [SerializeField] private GameObject timeSlider;
    [SerializeField] private Transform requestSlot;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    [SerializeField] private ParticleSystem correctParticle;
    [SerializeField] private ParticleSystem wrongParticle;
    [SerializeField] private int SingleIceCreamPrice = 25;
    [SerializeField] private int NapoleanIceCreamPrice = 70;
    [SerializeField] private int ShavedIcePrice = 50;
    [SerializeField] private int SingleWafflePrice = 100;
    [SerializeField] private int DoubleWafflePrice = 150;



    public void GetDelivered(GameObject recievedItem)
    {
        if (request.GetComponent<ShavedIce>())
        {
            if (DeliverManager.CompareShavedIce(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);
                WalletManager.Instance.SetMoney(WalletManager.Instance.Money + ShavedIcePrice);
                IncomeManager.Instance.AddSalesCount("Shaved Ice", 1);
            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
            }
        }
        else if (request.GetComponent<Cone>())
        {
            if (DeliverManager.CompareCone(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);

                if (request.GetComponent<Cone>().currentScoop >= 1)
                {
                    WalletManager.Instance.SetMoney(WalletManager.Instance.Money + NapoleanIceCreamPrice);
                    IncomeManager.Instance.AddSalesCount("Napolean Ice Cream", 1);
                }
                else if (request.GetComponent<Cone>().currentScoop < 1)
                {
                    WalletManager.Instance.SetMoney(WalletManager.Instance.Money + SingleIceCreamPrice);
                    IncomeManager.Instance.AddSalesCount("Single Scoop Ice Cream", 1);
                }

            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
            }
        }
        else if (request.GetComponent<Plate>())
        {
            if (DeliverManager.ComparePlate(request, recievedItem))
            {
                Interactable = false;
                Instantiate(correctParticle, transform.position, Quaternion.identity);

                if (request.GetComponent<Plate>().transform.childCount > 1)
                {
                    WalletManager.Instance.SetMoney(WalletManager.Instance.Money + DoubleWafflePrice);
                    IncomeManager.Instance.AddSalesCount("Double Waffle", 1);
                }
                else if (request.GetComponent<Plate>().transform.childCount == 1)
                {
                    WalletManager.Instance.SetMoney(WalletManager.Instance.Money + SingleWafflePrice);
                    IncomeManager.Instance.AddSalesCount("Single Waffle", 1);
                }


            }
            else
            {
                Interactable = false;
                Instantiate(wrongParticle, transform.position, Quaternion.identity);
                if (!isLeaving)
                {
                    Exit();
                }
            }
        }

        if (!isLeaving)
        {
            Exit();
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

        if (!isLeaving)
        {
            Instantiate(wrongParticle, transform.position, Quaternion.identity);
            Exit();
        }
    }
    private void Exit()
    {
        isLeaving = true;
        transform.GetChild(0).gameObject.SetActive(false);
        timeSlider.SetActive(false);
        timeSlider.GetComponent<Slider>().value = 0f;

        QueueManager.MoveQueue();
    }

}
