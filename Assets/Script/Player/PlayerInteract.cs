using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [field: SerializeField] public float interactDistance { get; private set; }
    [SerializeField] Transform objectPickupPoint;
    private Player player;
    private Transform cameraTransform;
    void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        player = FindFirstObjectByType<Player>();

        player.InputActions.Player.Attack.performed += InteractHandle;
    }

    private void InteractHandle(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, interactDistance))
        {
            if (objectPickupPoint.transform.childCount == 0) //Empty Hand
            {
                if (hitInfo.collider.TryGetComponent<IPickable>(out IPickable pickable))
                {
                    pickable.Pickup(objectPickupPoint);
                }

                else if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Interact(objectPickupPoint);
                }

            }
            else //Holding Something
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Placable")) //Place Item
                {
                    Transform child = objectPickupPoint.GetChild(0);
                    objectPickupPoint.DetachChildren();
                    child.rotation = Quaternion.identity;
                    StartCoroutine(lerpObject(child.position, hitInfo.point, child.transform));
                }

                else if (hitInfo.transform.TryGetComponent<ShavedIce>(out ShavedIce shavedIce) && objectPickupPoint.GetChild(0).TryGetComponent<Syrup>(out Syrup syrup))
                {
                    if (hitInfo.transform.gameObject.tag == "Unflavored")
                    {
                        shavedIce.AddFlavor(syrup.getFlavor());
                    }
                }

                else if (hitInfo.transform.TryGetComponent<Customer>(out Customer customer) && objectPickupPoint.GetChild(0).TryGetComponent<ShavedIce>(out ShavedIce shavedIce1))
                {
                    shavedIce1.give(hitInfo.transform.gameObject);
                }

                else if (hitInfo.transform.TryGetComponent<IceShavingMachine>(out IceShavingMachine iceShavingMachine) && objectPickupPoint.GetChild(0).TryGetComponent<EmptyCup>(out EmptyCup emptyCup))
                {
                    emptyCup.putInMachine(hitInfo.transform.gameObject);
                }

                else if (hitInfo.transform.TryGetComponent<IceShavingMachine>(out IceShavingMachine iceShavingMachine1) && objectPickupPoint.GetChild(0).TryGetComponent<IceBlock>(out IceBlock iceBlock))
                {
                    iceBlock.putInMachine(hitInfo.transform.gameObject);
                }

                else if (hitInfo.transform.TryGetComponent<TrashCan>(out TrashCan trashCan))
                {
                    Transform holdingItem = objectPickupPoint.GetChild(0);
                    if (!holdingItem.GetComponent<Syrup>())
                    {
                        trashCan.throwAwayItem(objectPickupPoint);
                    }
                }

            }
        }
    }

    void OnDestroy()
    {
        player.InputActions.Player.Disable();
    }

    private IEnumerator lerpObject(Vector3 startPostion, Vector3 targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion;
    }

}
