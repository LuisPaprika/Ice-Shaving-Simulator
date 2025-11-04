using UnityEngine;

public interface IInteractable
{
    void StopHovered();
    void Hovered();
    void Interact(GameObject transform, PlayerMovement player);
}
