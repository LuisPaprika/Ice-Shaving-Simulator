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
        return request.GetComponent<Cone>().flavors == recieved.GetComponent<Cone>().flavors;
    }
}
