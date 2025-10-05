using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IceShavingMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject shavedIcePrefab;
    [SerializeField] private GameObject timerSlider;
    [SerializeField] private GameObject iceCount;
    [SerializeField] private string interactPrompt;
    [SerializeField] private float iceShavingDuration;
    private int currentIce;
    [field: SerializeField] public Transform cupSlot { get; private set; }
    [field: SerializeField] public Transform iceSlot { get; private set; }

    public void Interact(GameObject transform, PlayerMovement player)
    {
        ShaveIce();
    }

    private void EnablingMachine(bool isEnabled)
    {
        gameObject.GetComponent<Collider>().enabled = isEnabled;
    }

    private void ShaveIce()
    {
        if (cupSlot.GetComponentInChildren<EmptyCup>() && currentIce > 0)
        {
            timerSlider.SetActive(true);
            EnablingMachine(false);

            Transform cup = cupSlot.GetChild(0);
            cup.GetComponent<Collider>().enabled = false;

            StartCoroutine(ShavingIce(iceShavingDuration));
        }
    }

    public void RefillIce()
    {
        iceCount.SetActive(true);
        currentIce = 5;
        iceCount.GetComponent<TextMeshProUGUI>().text = currentIce.ToString();
    }

    public void Hovered()
    {
        
    }

    private IEnumerator ShavingIce(float duration)
    {
        timerSlider.GetComponent<Slider>().maxValue = duration;
        timerSlider.GetComponent<Slider>().value = 0f;
        float currentTime = 0;

        while (timerSlider.GetComponent<Slider>().value < duration)
        {
            currentTime += Time.deltaTime;
            timerSlider.GetComponent<Slider>().value = currentTime;
            yield return null;
        }

        currentIce--;
        iceCount.GetComponent<TextMeshProUGUI>().text = currentIce.ToString();
        Destroy(cupSlot.GetChild(0).gameObject); //Destroy emptyCup
        GameObject obj = Instantiate(shavedIcePrefab, cupSlot.position, Quaternion.identity);
        obj.transform.SetParent(cupSlot);

        if (currentIce <= 0)
        {
            currentIce = 0;
            iceCount.SetActive(false);
            Destroy(iceSlot.GetChild(0).gameObject);
        }

        EnablingMachine(true);
        timerSlider.SetActive(false);

    }
}
