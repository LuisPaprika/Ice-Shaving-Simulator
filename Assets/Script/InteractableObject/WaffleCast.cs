using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaffleCast : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject timerSlider;
    [SerializeField] private float CookingDuration;
    [SerializeField] private GameObject batter;
    [SerializeField] private GameObject batterPosition;
    [SerializeField] private GameObject wafflePrefab;
    private bool haveBatter;
    public void Hovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        CookWaffle();
    }

    public void FillBatter()
    {
        haveBatter = true;
        batter.SetActive(haveBatter);
    }

    private void CookWaffle()
    {
        if (haveBatter)
        {
            haveBatter = false;
            StartCoroutine(CookingWaffle(CookingDuration));
        }
    }

    private IEnumerator CookingWaffle(float duration)
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

        batter.SetActive(false);
        GameObject obj = Instantiate(wafflePrefab, batterPosition.transform.position, Quaternion.identity);
        obj.transform.SetParent(batterPosition.transform);

        timerSlider.SetActive(false);
    }
}
