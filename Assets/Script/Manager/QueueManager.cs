using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class QueueManager : MonoBehaviour
{
    [field: SerializeField] public static int customerCount { get; private set; }
    [field: SerializeField] public static GameObject[] Positions { get; private set; }
    public static Queue<GameObject> CustomerQueue = new Queue<GameObject>();

    public static void MoveQueue()
    {
        for (int i = 0; i < customerCount; i++)
        {
            CustomerQueue.Peek().GetComponent<NavMeshAgent>().SetDestination(Positions[i].transform.position);
            CustomerQueue.Enqueue(CustomerQueue.Dequeue());
        }
        CustomerQueue.Dequeue();
        customerCount--;
    }

    public static void SetPositions(GameObject[] positions)
    {
        Positions = positions;
    }

    public static void SetCustomerCount(int count)
    {
        customerCount = count;
    }
}
