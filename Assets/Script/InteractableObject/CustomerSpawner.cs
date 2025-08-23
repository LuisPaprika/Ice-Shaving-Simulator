using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform waitPosition;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] float spawnInitial;
    private bool allowSpawning;

    void Awake()
    {
        allowSpawning = true;
        StartSpawning();
    }
    private void StartSpawning()
    {
        allowSpawning = true;
        StartCoroutine(spawnCustomer());
    }

    private IEnumerator spawnCustomer()
    {
        while (allowSpawning)
        {
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            Customer script = customer.GetComponent<Customer>();
            script.Init(waitPosition, endPosition);
            yield return new WaitForSeconds(spawnInitial);
        }
    }
}
