using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform waitPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitingTime;
    [SerializeField] private float spawnInitial;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private ShaveIcedAsset shaveIcedAsset;
    private bool allowSpawning;

    void Awake()
    {
        OpeningSign.onOpenStore += StartSpawning;
        OpeningSign.onCloseStore += StopSpawning;
    }

    private void StartSpawning()
    {
        allowSpawning = true;
        StartCoroutine(spawnCustomer(Random.Range(2, 5)));
    }

    private void StopSpawning()
    {
        allowSpawning = false;
    }

    private IEnumerator spawnCustomer(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        while (allowSpawning)
        {
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            Customer script = customer.GetComponent<Customer>();
            script.Init(shaveIcedAsset.getRandomFlavor(), waitPosition, endPosition, waitingTime, moveSpeed);
            yield return new WaitForSeconds(spawnInitial);
        }
    }

    void OnDestroy()
    {
        OpeningSign.onOpenStore -= StopSpawning;
        OpeningSign.onCloseStore -= StopSpawning;
    }

}
