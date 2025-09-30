using UnityEngine;

public class BillboardEffect : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.forward = Camera.main.transform.forward;
    }
}
