using System.Collections;
using UnityEngine;

public class IceCreamBox : Pickable
{
    [field: SerializeField] public IceCreamFlavor iceCreamFlavor;
    [field: SerializeField] public int scoopAmount = 20;

    public void AddIceCream(GameObject targetStack)
    {
        targetStack.GetComponent<IceCream>().AddScoop(scoopAmount);
        StartCoroutine(stackObject(transform.position, targetStack, transform));
    }

    private IEnumerator stackObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion.transform.position, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj.gameObject);
    }
}
