using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [field: SerializeField] public float InteractDistance { get; private set; }
    [SerializeField] private GameObject objectPickupPoint;
    [SerializeField] private PlayerMovement player;
    private Transform cameraTransform;

    private GameObject lookingObj;
    private GameObject pastObj;
    void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        player.InputActions.Player.Attack.performed += InteractHandle;
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
                    Transform child = objectPickupPoint.transform.GetChild(0);
                    if (!child.transform.CompareTag("Tools"))
                    {
                        objectPickupPoint.transform.DetachChildren();
                        child.rotation = Quaternion.identity;
                        if (hitInfo.transform.CompareTag("Container"))
                        {
                            StartCoroutine(PutObjectInContainer(child.position, hitInfo.point, child.transform, hitInfo.transform));
                        }
                        else
                        {
                            StartCoroutine(PlaceObject(child.position, hitInfo.point, child.transform, hitInfo.transform));
                        }
                    }
                    else
                    {
                        if (!hitInfo.transform.CompareTag("Container"))
                        {
                            objectPickupPoint.transform.DetachChildren();
                            child.rotation = Quaternion.identity;
                            StartCoroutine(PlaceObject(child.position, hitInfo.point, child.transform, hitInfo.transform));
                        }
                    }
                    
                }

                else if (hitInfo.transform.TryGetComponent(out TrashCan trashCan))
                {
                    Transform holdingItem = objectPickupPoint.transform.GetChild(0);
                    if (!holdingItem.GetComponent<Syrup>())
                    {
                        trashCan.throwAwayItem(objectPickupPoint);
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out ShavedIce shavedIce) && objectPickupPoint.transform.GetChild(0).TryGetComponent(out Syrup syrup))
                {
                    if (hitInfo.transform.gameObject.tag == "Unflavored")
                    {
                        shavedIce.AddFlavor(syrup.getFlavor());
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out WaffleBatter waffleBatter) && objectPickupPoint.transform.GetChild(0).TryGetComponent(out Laddle laddle))
                {
                    if (waffleBatter.BatterLeft > 0)
                    {
                        waffleBatter.Dip(laddle);
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out WaffleCast waffleCast) && objectPickupPoint.transform.GetChild(0).TryGetComponent(out Laddle laddle1))
                {
                    if (laddle1.isFull && !waffleCast.haveBatter)
                    {
                        laddle1.putInMachine(waffleCast);
                    }
                }

                else if (hitInfo.transform.TryGetComponent(out Customer customer))
                {
                    if (customer.Interactable)
                    {
                        if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out ShavedIce shavedIce1))
                        {
                            shavedIce1.Give(hitInfo.transform.gameObject);
                        }
                        else if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out Cone cone))
                        {
                            cone.Give(hitInfo.transform.gameObject);
                        }
                        else if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out Plate plate))
                        {
                            plate.Give(hitInfo.transform.gameObject);
                        }
                    }
                }

                else if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out EmptyCup emptyCup))
                {
                    if (hitInfo.transform.GetComponent<IceShavingMachine>() ||
                        hitInfo.transform.GetComponent<ShavingStand>())
                    {
                        emptyCup.putInMachine(hitInfo.transform.gameObject);
                    }
                }

                else if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out IceBlock iceBlock))
                {
                    if (hitInfo.transform.GetComponent<IceShavingMachine>() ||
                        hitInfo.transform.GetComponent<ShavingStand>())
                    {
                        iceBlock.putInMachine(hitInfo.transform.gameObject);
                    }
                }

                else if (objectPickupPoint.transform.GetChild(0).TryGetComponent(out Cone cone))
                {
                    if (hitInfo.transform.TryGetComponent(out IceCream iceCream))
                    {
                        if (cone.currentScoop < 3)
                        {
                            iceCream.Scoop(cone.gameObject);
                        }
                    }
                }

            }
        }
    }

    void OnDestroy()
    {
        player.InputActions.Player.Disable();
    }

    private IEnumerator PlaceObject(Vector3 startPostion, Vector3 targetPostion, Transform placeObj, Transform targetObj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            placeObj.position = Vector3.Lerp(startPostion, targetPostion, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        placeObj.position = targetPostion;
    }

    private IEnumerator PutObjectInContainer(Vector3 startPostion, Vector3 targetPostion, Transform placeObj, Transform targetObj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            placeObj.position = Vector3.Lerp(startPostion, targetPostion, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        placeObj.position = targetPostion;

        placeObj.SetParent(targetObj);
        targetObj.GetComponent<Plate>().UpdateIngredient();
    }

}
