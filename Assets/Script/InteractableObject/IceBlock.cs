using System.Collections;
using UnityEngine;

public class IceBlock : Pickable, IInteractable
{
    [SerializeField] private string interactPrompt;
    private bool inMachine = false;

    public void Interact(GameObject transform, PlayerMovement player)
    {

    }

    public void putInMachine(GameObject targetMachine)
    {
        inMachine = true;
        if (targetMachine.TryGetComponent<IceShavingMachine>(out IceShavingMachine iceShavingMachine))
        {
            if (iceShavingMachine.iceSlot.childCount == 0)
            {
                iceShavingMachine.RefillIce();

                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, iceShavingMachine.iceSlot.gameObject, transform));
                transform.SetParent(iceShavingMachine.iceSlot.transform);
            }
        }
        else if (targetMachine.TryGetComponent<ShavingStand>(out ShavingStand shavingStand))
        {
            if (shavingStand.IceSlot.childCount == 0)
            {
                shavingStand.RefillIce();

                transform.rotation = Quaternion.identity;
                StartCoroutine(lerpObject(transform.position, shavingStand.IceSlot.gameObject, transform));
                transform.SetParent(shavingStand.IceSlot.transform);
            }
        }

    }

    public void Hovered()
    {

    }
    
    public void StopHovered()
    {

    }

    public void Grab()
    {
        Debug.Log("Grabbing");
    }

    public override void Pickup(GameObject objectPickupPoint)
    {
        if (!inMachine)
        {
            StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
            transform.SetParent(objectPickupPoint.transform);
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
