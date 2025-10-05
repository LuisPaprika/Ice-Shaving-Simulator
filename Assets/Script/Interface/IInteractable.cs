using System;
using UnityEngine;

public interface IInteractable
{
    void Hovered();
    void Interact(GameObject transform, PlayerMovement player);
}
