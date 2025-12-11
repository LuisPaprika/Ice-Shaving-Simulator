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
        if (!OpeningSign.isOpen)
        {
            DayManager.SetDay(DayManager.Day + 1);
        }
        else
        {
            StartCoroutine(UIManager.Instance.ShowAnouncingText("I should close the store first"));
        }
        
    }

}
