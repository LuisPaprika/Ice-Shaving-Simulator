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
        DayManager.SetDay(DayManager.Day + 1);
    }

}
