using UnityEngine;

public class HoveringInteractable : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    void FixedUpdate()
    {
        RaycastHitHandle();
    }

    private void RaycastHitHandle()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, playerInteract.InteractDistance))
        {
            if (hitInfo.transform.TryGetComponent<IInteractable>(out IInteractable interactable) ||
                hitInfo.transform.TryGetComponent<Pickable>(out Pickable pickable))
            {
                UIManager.Instance.ChangeCrosshair(Color.black);
            }
            else
            {
                UIManager.Instance.ChangeCrosshair(Color.white);
            }
        }
        else
        {
            UIManager.Instance.ChangeCrosshair(Color.white);
        }
    }
}
