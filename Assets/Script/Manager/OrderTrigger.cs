using UnityEngine;

public class OrderTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Customer>(out Customer customer))
        {
            customer.StartWaiting();
        }
    }
}
