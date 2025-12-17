using System.Collections;
using UnityEngine;

public class Laddle : Pickable
{
    [SerializeField] private GameObject batter;
    [field:SerializeField] public bool isFull { get; private set; }

    private IEnumerator lerpObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion.transform.position, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion.transform.position;
    }

    public void toggleBatter()
    {
        isFull = !isFull;
        batter.SetActive(isFull);
    }

    public void putInMachine(WaffleCast waffleCast)
    {
        waffleCast.FillBatter();
        toggleBatter();
    }
}
