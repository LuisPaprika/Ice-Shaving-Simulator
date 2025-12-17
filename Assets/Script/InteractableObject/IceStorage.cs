using System;
using System.Collections;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class IceStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject iceBlockPrefab;
    [SerializeField] private string interactPrompt;
    [field: SerializeField] public int maxIce {get; private set;} = 5;
    [field: SerializeField] public int currentIce {get; private set;}
    [SerializeField] private TextMeshProUGUI iceCountUI;

    void Awake()
    {
        currentIce = maxIce;
        iceCountUI.text = currentIce.ToString();
    }
    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void AddToStack()
    {
        currentIce++;
        iceCountUI.text = currentIce.ToString();
    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        pickupIce(objectPickupPoint);
    }

    private void pickupIce(GameObject objectPickupPoint)
    {
        if (currentIce > 0)
        {
            currentIce--;
            iceCountUI.text = currentIce.ToString();
            GameObject icePack = Instantiate(iceBlockPrefab, transform.position, Quaternion.identity, objectPickupPoint.transform);
            StartCoroutine(lerpObject(transform.position, objectPickupPoint, icePack.transform));
        }

    }

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
}
