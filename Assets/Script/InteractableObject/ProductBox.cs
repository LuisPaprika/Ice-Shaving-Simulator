using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ProductBox : MonoBehaviour, IInteractable
{
    [field: SerializeField] public static ProductBox Instance { get; private set; }
    private List<Order> products = new List<Order>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void Interact(GameObject objectPickupPoint, PlayerMovement player)
    {
        if (products.Count != 0)
        {
            SpawnProduct(objectPickupPoint);
        }
    }

    public void Hovered()
    {

    }

    public void StopHovered()
    {

    }

    public void AddProducts(Order receivedOrder)
    {
        if(products.Count == 0)
        {
            products.Add(receivedOrder);
            return;
        }

        foreach(Order order in products)
        {
            if(order.product.productName == receivedOrder.product.productName)
            {
                order.amount++;
                return;
            }
        }

        products.Add(receivedOrder);
    }

    private void SpawnProduct(GameObject pickupPoint)
    {
        Order order = products[0];
        GameObject product = Instantiate(order.product.prefab, transform.position, Quaternion.identity);
        product.GetComponent<Pickable>().Pickup(pickupPoint);
        order.amount--;
        if (order.amount == 0)
        {
            products.Remove(order);
        }
    }

}
