using UnityEngine;

public class HoveringInteractable : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    private Transform cameraTransform;
    void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        RaycastHitHandle();
    }

    private void RaycastHitHandle()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, playerInteract.InteractDistance))
        {
            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Hovered();
            }
        }
    }
}
