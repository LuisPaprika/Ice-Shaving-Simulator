using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningSign : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator flipAnimator;
    [field: SerializeField] public static bool isOpen { get; private set; }
    public static event Action onOpenStore;
    public static event Action onCloseStore;

    void Awake()
    {
        isOpen = false;
    }

    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        playFlipAnimation();
    }

    private void playFlipAnimation()
    {
        isOpen = !isOpen;
        UIManager.Instance.ToggleOpenCloseText(isOpen);
        if (isOpen)
        {
            flipAnimator.SetTrigger("Open");
            StartCoroutine(delayOpenInvoke(2f));
        }
        else
        {
            flipAnimator.SetTrigger("Close");
            onCloseStore?.Invoke();
        }
    }

    private IEnumerator delayOpenInvoke(float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            if (!isOpen)
            {
                yield break;
            }
            currentTime += Time.deltaTime;

            yield return null;
        }
        onOpenStore?.Invoke();
    }

    
}
