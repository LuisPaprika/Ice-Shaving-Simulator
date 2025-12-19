using UnityEngine;

[CreateAssetMenu(fileName = "ProductSO", menuName = "Scriptable Objects/ProductSO")]
public class ProductSO : ScriptableObject
{
    public string productName;
    public int price;
    public GameObject prefab;
}
