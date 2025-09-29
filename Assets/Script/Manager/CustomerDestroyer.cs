using UnityEngine;

public class CustomerDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Customer>())
        {
            Destroy(other.gameObject);
            QueueManager.CustomerQueue.Dequeue();
        }
    }
}
