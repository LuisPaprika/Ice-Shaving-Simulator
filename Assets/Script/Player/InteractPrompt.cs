using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPrompt : MonoBehaviour
{
    private TMP_Text prompt;
    private Player player;
    void Awake()
    {
        player = FindFirstObjectByType<Player>();

        prompt = gameObject.GetComponent<TMP_Text>();

        Syrup.onHovered += setText;
        ShavedIce.onHovered += setText;
        EmptyCup.onHovered += setText;
        IceShavingMachine.onHovered += setText;
    }

    void Update()
    {
        prompt.text = "";
    }

    private void setText(string text)
    {
        prompt.text = "Press " + GetActionKey("Interact") + "\nto " + text;
    }

    private string GetActionKey(string ActionName)
    {
        InputAction action = player.InputActions.FindAction(ActionName);
        return InputControlPath.ToHumanReadableString(action.bindings[0].path);
    }

    void OnDestroy()
    {
        Syrup.onHovered -= setText;
        ShavedIce.onHovered -= setText;
        EmptyCup.onHovered -= setText;
        IceShavingMachine.onHovered -= setText;
    }
}
