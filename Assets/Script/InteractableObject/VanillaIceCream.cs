using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VanillaIceCream : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject progressSlider;
    [SerializeField] private GameObject vanillaScoopPrefab;
    [SerializeField] private float scoopTime = 0.5f;
    private bool isLooked;
    private bool interactable = true;
    private IceCreamFlavor flavor = IceCreamFlavor.Vanilla;

    private void FixedUpdate()
    {
        isLooked = false;
    }

    public void Hovered()
    {
        isLooked = true;
    }

    public void Interact(Transform objectPickupPoint, PlayerMovement player)
    {

    }

    public void Scoop(GameObject cone)
    {
        if (interactable)
        {
            StartCoroutine(StartScooping(scoopTime, cone));
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
        GameObject scoop = Instantiate(vanillaScoopPrefab, transform.position, Quaternion.identity, transform);
        cone.GetComponent<Cone>().AddScoop(scoop, flavor);
    }
}
