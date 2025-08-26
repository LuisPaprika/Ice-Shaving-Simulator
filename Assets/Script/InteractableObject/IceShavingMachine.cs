using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class IceShavingMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject shavedIcePrefab;
    [SerializeField] private string interactPrompt;
    [SerializeField] private float iceShavingDuration;
    private int currentIce;
    public static event Action<string> onHovered;
    [field: SerializeField] public Transform cupSlot { get; private set; }
    [field: SerializeField] public Transform iceSlot { get; private set; }

    public void Interact(Transform transform, PlayerMovement player)
    {
        shaveIce();
    }

    private void enablingMachine(bool isEnabled)
    {
        gameObject.GetComponent<Collider>().enabled = isEnabled;
    }

    private void shaveIce()
    {
        if (cupSlot.childCount > 0 && currentIce > 0)
        {
            enablingMachine(false);

            Transform cup = cupSlot.GetChild(0);
            cup.GetComponent<Collider>().enabled = false;

            StartCoroutine(shavingIce(iceShavingDuration));
        }
    }

    public void RefillIce()
    {
        currentIce = 5;
    }

    public void Hovered()
    {
        if (cupSlot.childCount > 0 && currentIce > 0)
        {
            onHovered?.Invoke(interactPrompt);
        }
    }

    private IEnumerator shavingIce(float duration)
    {
        yield return new WaitForSeconds(duration);

        currentIce--;
        Destroy(cupSlot.GetChild(0).gameObject); //Destroy emptyCup
        Instantiate(shavedIcePrefab, cupSlot.position, Quaternion.identity);

        if (currentIce <= 0)
        {
            currentIce = 0;
            Destroy(iceSlot.GetChild(0).gameObject);
        }

        enablingMachine(true);

    }
}
