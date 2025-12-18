using UnityEngine;

public class HoveringInteractable : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    private CanvasDisplay tempScript;
    private GameObject lookedObject;
    void Update()
    {
        RaycastHitHandle();
    }

    private void RaycastHitHandle()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, playerInteract.InteractDistance))
        {
            if (lookedObject != null && lookedObject != hitInfo.collider.gameObject)
            {
                if (tempScript)
                {
                    tempScript.EnablingCanvas(false);
                }
            }
            else if (lookedObject == hitInfo.collider.gameObject)
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out CanvasDisplay canvasDisplay))
                {
                    tempScript = canvasDisplay;
                    canvasDisplay.EnablingCanvas(true);
                }
            }

            lookedObject = hitInfo.collider.gameObject;

            if (hitInfo.transform.TryGetComponent(out IInteractable interactable) ||
                hitInfo.transform.TryGetComponent(out Pickable pickable))
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
            if (lookedObject != null)
            {
                if (tempScript)
                {
                    tempScript.EnablingCanvas(false);
                }
                
                lookedObject = null;
            }

            UIManager.Instance.ChangeCrosshair(Color.white);
        }
    }
}
