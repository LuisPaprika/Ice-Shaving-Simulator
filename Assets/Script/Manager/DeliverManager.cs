using System.Collections.Generic;
using UnityEngine;

public class DeliverManager : MonoBehaviour
{
    public static bool CompareShavedIce(GameObject request, GameObject recieved)
    {
        if (!recieved.GetComponent<ShavedIce>())
        {
            return false;
        }
        return request.GetComponent<ShavedIce>().flavor == recieved.GetComponent<ShavedIce>().flavor;
    }

    public static bool CompareCone(GameObject request, GameObject recieved)
    {
        if (!recieved.GetComponent<Cone>())
        {
            return false;
        }

        return request.GetComponent<Cone>().chocCount == recieved.GetComponent<Cone>().chocCount &&
                request.GetComponent<Cone>().vanilCount == recieved.GetComponent<Cone>().vanilCount &&
                request.GetComponent<Cone>().strawCount == recieved.GetComponent<Cone>().strawCount;
    }

    public static bool ComparePlate(GameObject request, GameObject recieved)
    {
        if (!recieved.GetComponent<Plate>())
        {
            return false;
        }

        if (recieved.transform.childCount != request.transform.childCount)
        {
            return false;
        }

        foreach(Transform child in request.transform)
        {
            foreach(Transform child2 in recieved.transform)
            {
                if(child.GetComponent<Ingredient>().ingredientSO != child2.GetComponent<Ingredient>().ingredientSO)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
