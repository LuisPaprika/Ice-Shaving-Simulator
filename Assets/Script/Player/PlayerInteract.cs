using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [field: SerializeField] public float InteractDistance { get; private set; }
    [SerializeField] Transform objectPickupPoint;
    [SerializeField] PlayerMovement player;
    private Transform cameraTransform;
    void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        player.InputActions.Player.Attack.performed += InteractHandle;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, InteractDistance))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Hovered();
            }
        }
    }

    private void InteractHandle(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, InteractDistance))
        {
            if (objectPickupPoint.transform.childCount == 0) //Empty Hand
            {
                if (hitInfo.collider.TryGetComponent(out IPickable pickable))
                {
                    pickable.Pickup(objectPickupPoint);
                }

                else if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(objectPickupPoint, player);
                }

            }
            else //Holding Something
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Placable")) //Place Item
                {
                    Transform child = objectPickupPoint.GetChild(0);
                    objectPickupPoint.DetachChildren();
                    child.rotation = Quaternion.identity;
                    StartCoroutine(PlaceObject(child.position, hitInfo.point, child.transform));
                }

                else if (hitInfo.transform.TryGetComponent(out ShavedIce shavedIce) && objectPickupPoint.GetChild(0).TryGetComponent<Syrup>(out Syrup syrup))
                {
                    if (hitInfo.transform.gameObject.tag == "Unflavored")
                    {
                        shavedIce.AddFlavor(syrup.getFlavor());
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out Customer customer) && objectPickupPoint.GetChild(0).TryGetComponent<ShavedIce>(out ShavedIce shavedIce1))
                {
                    if (customer.Interactable)
                    {
                        shavedIce1.Give(hitInfo.transform.gameObject);
                    }
                }

                else if (objectPickupPoint.GetChild(0).TryGetComponent(out EmptyCup emptyCup))
                {
                    if (hitInfo.transform.GetComponent<IceShavingMachine>() ||
                        hitInfo.transform.GetComponent<ShavingStand>())
                    {
                        emptyCup.putInMachine(hitInfo.transform.gameObject);
                    }
                }

                else if (objectPickupPoint.GetChild(0).TryGetComponent(out IceBlock iceBlock))
                {
                    if (hitInfo.transform.GetComponent<IceShavingMachine>() ||
                        hitInfo.transform.GetComponent<ShavingStand>())
                    {
                        iceBlock.putInMachine(hitInfo.transform.gameObject);
                    }
                }

                else if (objectPickupPoint.GetChild(0).TryGetComponent(out Cone cone))
                {
                    if (hitInfo.transform.TryGetComponent(out VanillaIceCream vanillaIceCream))
                    {
                        if (cone.currentScoop < 3)
                        {
                            vanillaIceCream.Scoop(cone.gameObject);
                        }
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out TrashCan trashCan))
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

    private IEnumerator PlaceObject(Vector3 startPostion, Vector3 targetPostion, Transform obj)
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
