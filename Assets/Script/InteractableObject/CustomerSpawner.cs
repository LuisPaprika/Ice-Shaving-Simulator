using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] positions; //index 0 is destroy position
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitingTime;
    [SerializeField] private float spawnInitial;
    [SerializeField] private GameObject customerPrefab;
    private bool allowSpawning;

    void Awake()
    {
        OpeningSign.onOpenStore += StartSpawning;
        OpeningSign.onCloseStore += StopSpawning;

        QueueManager.SetPositions(positions);
    }

    private void StartSpawning()
    {
        allowSpawning = true;
        StartCoroutine(SpawnCustomer(Random.Range(2, 5)));
    }

    private void StopSpawning()
    {
        allowSpawning = false;
    }

    private IEnumerator SpawnCustomer(int startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        while (allowSpawning && QueueManager.customerCount < 4)
        {
            QueueManager.SetCustomerCount(QueueManager.customerCount + 1);
    
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            QueueManager.CustomerQueue.Enqueue(customer);

            Customer script = customer.GetComponent<Customer>();
            script.Init(OrderAssets.Instance.GetRandomOrder(), waitingTime, QueueManager.Positions[QueueManager.customerCount].transform);

            yield return new WaitForSeconds(spawnInitial);
        }
    }

    void OnDestroy()
    {
        OpeningSign.onOpenStore -= StopSpawning;
        OpeningSign.onCloseStore -= StopSpawning;
    }

}
