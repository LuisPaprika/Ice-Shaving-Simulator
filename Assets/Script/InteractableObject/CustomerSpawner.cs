using System;
using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] positions; //index 0 is destroy position
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitingTime;
    [SerializeField] private float spawnInitial;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int baseCustomerLimit;
    [SerializeField] private float customerLimitGrowth;
    [field: SerializeField] public static CustomerSpawner Instance {get; private set;}
    private bool allowSpawning;
    private int spawnedCustomer;
    private int customerLimitPerDay;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }


        OpeningSign.onOpenStore += StartSpawning;
        OpeningSign.onCloseStore += StopSpawning;

        QueueManager.SetPositions(positions);
    }

    private void StartSpawning()
    {
        allowSpawning = true;
        StartCoroutine(SpawnCustomer(UnityEngine.Random.Range(2, 5)));
    }

    private void StopSpawning()
    {
        allowSpawning = false;
    }

    public void SetCustomerLimit(int day)
    {
        customerLimitPerDay = Mathf.RoundToInt(baseCustomerLimit + (customerLimitGrowth * MathF.Log(day + 1)));
        spawnedCustomer = 0;
    }

    private IEnumerator SpawnCustomer(int startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        while (allowSpawning)
        {
            while (QueueManager.customerCount < 4 && (spawnedCustomer <= customerLimitPerDay) && allowSpawning)
            {
                QueueManager.SetCustomerCount(QueueManager.customerCount + 1);

                GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
                spawnedCustomer++;
                QueueManager.CustomerQueue.Enqueue(customer);

                Customer script = customer.GetComponent<Customer>();
                script.Init(OrderAssets.Instance.GetRandomOrder(), waitingTime, QueueManager.Positions[QueueManager.customerCount].transform);

                yield return new WaitForSeconds(spawnInitial);
            }
            yield return null;
        }

    }

    void OnDestroy()
    {
        OpeningSign.onOpenStore -= StopSpawning;
        OpeningSign.onCloseStore -= StopSpawning;
    }

}
