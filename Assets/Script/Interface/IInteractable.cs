using UnityEngine;

public interface IInteractable
{
    void StopHovered();
    void Hovered();
    void Interact(GameObject objectPickupPoint, PlayerMovement player);
}
