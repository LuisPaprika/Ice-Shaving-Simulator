using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IceCream : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject progressSlider;
    [SerializeField] private GameObject scoopPrefab;
    [SerializeField] private float scoopTime = 0.5f;
    private bool isLooked;
    private bool interactable = true;
    [SerializeField] private IceCreamFlavor flavor;
    [SerializeField] private TextMeshProUGUI scoopCountUI;
    [field: SerializeField] public int CurrentScoop { get; private set; }
    [field: SerializeField] public int MaxScoop { get; private set; } = 30;

    void Awake()
    {
        CurrentScoop = MaxScoop;
        scoopCountUI.text = CurrentScoop.ToString();
    }

    private void Update()
    {
        isLooked = false;
    }

    public void Hovered()
    {
        isLooked = true;
    }

    public void StopHovered()
    {

    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {

    }

    public void Scoop(GameObject cone)
    {
        if (CurrentScoop > 0)
        {
            CurrentScoop--;
            scoopCountUI.text = CurrentScoop.ToString();
            GameObject scoop = Instantiate(scoopPrefab, transform.position, Quaternion.identity, transform);
            cone.GetComponent<Cone>().AddScoop(scoop, flavor);
        }

    }

    private IEnumerator StartScooping(float duration, GameObject cone)
    {
        progressSlider.SetActive(true);

        float currentTime = 0f;
        progressSlider.GetComponent<Slider>().maxValue = duration;

        while (isLooked && currentTime < duration)
        {
            interactable = false;
            currentTime += Time.deltaTime;
            progressSlider.GetComponent<Slider>().value = currentTime;

            yield return null;
        }

        if (isLooked && currentTime >= duration)
        {
            _Scoop(cone);
            isLooked = false;
        }

        interactable = true;
        progressSlider.SetActive(false);

    }

    private void _Scoop(GameObject cone)
    {
        GameObject scoop = Instantiate(scoopPrefab, transform.position, Quaternion.identity, transform);
        cone.GetComponent<Cone>().AddScoop(scoop, flavor);
    }
}
