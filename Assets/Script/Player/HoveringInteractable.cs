using UnityEngine;

public class HoveringInteractable : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    void Update()
    {
        RaycastHitHandle();
    }

    private void RaycastHitHandle()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, playerInteract.InteractDistance))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
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
