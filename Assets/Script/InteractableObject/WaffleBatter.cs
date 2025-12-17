using TMPro;
using UnityEngine;

public class WaffleBatter : MonoBehaviour, IInteractable
{
    [field: SerializeField] public int CurrentBatter { get; private set; }
    [field: SerializeField] public int MaxBatter { get; private set; } = 5;
    [SerializeField] private TextMeshProUGUI batterCountUI;

    void Awake()
    {
        CurrentBatter = MaxBatter;
        batterCountUI.text = CurrentBatter.ToString();
    }
    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

    }

    public void AddBatter(Laddle laddle)
    {
        CurrentBatter++;
        batterCountUI.text = CurrentBatter.ToString();
        laddle.toggleBatter();
    }

    public void Dip(Laddle laddle)
    {
        if(CurrentBatter > 0)
        {
            CurrentBatter--;
            batterCountUI.text = CurrentBatter.ToString();
            laddle.toggleBatter();
        }
        
    }
}
