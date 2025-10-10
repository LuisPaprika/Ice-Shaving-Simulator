using UnityEngine;

public class WaffleBatter : MonoBehaviour, IInteractable
{
    [field: SerializeField] public int BattleLeft { get; private set; } = 5;
    public void Hovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

    }

    public void Dip(Laddle laddle)
    {
        laddle.toggleBatter();
    }
}
