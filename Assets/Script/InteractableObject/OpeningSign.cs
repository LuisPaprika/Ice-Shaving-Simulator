using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningSign : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator flipAnimator;
    private bool isOpen = false;
    public static event Action onOpenStore;
    public static event Action onCloseStore;

    public void Hovered()
    {

    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
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
