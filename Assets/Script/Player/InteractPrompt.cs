using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPrompt : MonoBehaviour
{
    private TMP_Text prompt;
    private PlayerMovement player;
    void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>();

        prompt = gameObject.GetComponent<TMP_Text>();
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

}
