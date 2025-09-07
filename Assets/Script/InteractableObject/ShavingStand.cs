using UnityEngine;

public class ShavingStand : MonoBehaviour, IInteractable
{
    private int currentIce;
    [SerializeField] private Transform cameraPosition;
    [field: SerializeField] public Transform CupSlot { get; private set; }
    [field: SerializeField] public Transform IceSlot { get; private set; }

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
        if (CupSlot.childCount > 0 && currentIce > 0)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerInteract>().enabled = false;

            player.GetComponent<ShavingStandMode>().enabled = true;
            player.GetComponent<ShavingStandMode>().Init(player, cameraPosition);
        }
    }
}
