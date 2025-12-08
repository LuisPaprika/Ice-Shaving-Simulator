using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour, IPickable
{
    [SerializeField] private List<ScriptableObject> ingredients;

    public void UpdateIngredient()
    {
        ingredients.Clear();
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent<Ingredient>(out Ingredient ingredient))
            {
                ingredients.Add(ingredient.ingredientSO);
            }
        }
    }

    public void Pickup(GameObject objectPickupPoint)
    {
        StartCoroutine(lerpObject(transform.position, objectPickupPoint.gameObject, transform));
        transform.SetParent(objectPickupPoint.transform);
    }

    private IEnumerator lerpObject(Vector3 startPostion, GameObject targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startPostion, targetPostion.transform.position, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion.transform.position;
    }
}
