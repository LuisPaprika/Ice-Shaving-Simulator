using UnityEngine;

public class ShavingStand : MonoBehaviour, IInteractable
{
    private int currentIce;
    [field: SerializeField] public Transform cupSlot { get; private set; }
    [field: SerializeField] public Transform iceSlot { get; private set; }

    public void Hovered()
    {

    }

    public void Interact(Transform transform, PlayerMovement player)
    {
        EnterShavingMode(player);
    }

    public void RefillIce()
    {
        currentIce += 5;
    }

    private void EnterShavingMode(PlayerMovement player)
    {
        if (cupSlot.childCount > 0 && currentIce > 0)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
