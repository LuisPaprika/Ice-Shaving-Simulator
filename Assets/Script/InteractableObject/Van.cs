using UnityEngine;

public class Van : MonoBehaviour, IInteractable
{
    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        if (!PlayerMovement.Instance.isMovementDisabled)
        {
            if (!OpeningSign.isOpen)
            {
                UIManager.Instance.CreateSales();
            }
            else
            {
                StartCoroutine(UIManager.Instance.ShowAnouncingText("I should close the store first"));
            }
        }

    }

}
