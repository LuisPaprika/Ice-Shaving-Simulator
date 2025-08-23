using System;
using UnityEngine;

public interface IInteractable
{
    void Hovered();
    void Interact(Transform transform);
}
