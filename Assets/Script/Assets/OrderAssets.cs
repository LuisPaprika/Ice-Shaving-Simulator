using System.Collections.Generic;
using UnityEngine;

public class OrderAssets : MonoBehaviour
{
    [SerializeField] private List<GameObject> menu;

    // [SerializeField] private GameObject strawberryShavedIce;
    // [SerializeField] private GameObject chocolateShavedIce;
    // [SerializeField] private GameObject milkShavedIce;

    // [SerializeField] private GameObject coneStraw;
    // [SerializeField] private GameObject coneVanil;
    // [SerializeField] private GameObject coneChoco;

    // [SerializeField] private GameObject coneVanilVanil;
    // [SerializeField] private GameObject coneStrawStraw;
    // [SerializeField] private GameObject coneChocoChoco;
    // [SerializeField] private GameObject coneVanilChoco;
    // [SerializeField] private GameObject coneVanilStraw;
    // [SerializeField] private GameObject coneChocoStraw;

    // [SerializeField] private GameObject coneStrawStrawStraw;
    // [SerializeField] private GameObject coneChocoChocoChoco;
    // [SerializeField] private GameObject coneVanilVanilVanil;
    // [SerializeField] private GameObject coneVanilVanilStraw;
    // [SerializeField] private GameObject coneVanilVanilChoco;
    // [SerializeField] private GameObject coneChocoChocoStraw;
    // [SerializeField] private GameObject coneChocoChocoVanil;
    // [SerializeField] private GameObject coneStrawStrawChoco;
    // [SerializeField] private GameObject coneStrawStrawVanil;
    // [SerializeField] private GameObject coneVanilChocoStraw;

    [field: SerializeField] public static OrderAssets Instance { get; private set; }


    private GameObject GetOrderAsset(GameObject order)
    {
        if (order.TryGetComponent(out ShavedIce shavedIce))
        {
            switch (shavedIce.flavor)
            {
                case ShavedIceFlavor.Strawberry:
                    return menu[0];
                case ShavedIceFlavor.Chocolate:
                    return menu[1];
                case ShavedIceFlavor.Milk:
                    return menu[2];

                default:
                    return null;
            }
        }
        else if (order.TryGetComponent(out Cone cone))
        {
            var key = (cone.flavors[IceCreamFlavor.Vanilla], cone.flavors[IceCreamFlavor.Chocolate], cone.flavors[IceCreamFlavor.Strawberry]);
            switch (key)
            {
                case (0, 0, 1):
                    return menu[3];
                case (0, 1, 0):
                    return menu[4];
                case (1, 0, 0):
                    return menu[5];
                case (2, 0, 0):
                    return menu[6];
                case (0, 0, 2):
                    return menu[7];
                case (0, 2, 0):
                    return menu[8];
                case (1, 1, 0):
                    return menu[9];
                case (1, 0, 1):
                    return menu[10];
                case (0, 1, 1):
                    return menu[11];
                case (0, 0, 3):
                    return menu[12];
                case (0, 3, 0):
                    return menu[13];
                case (3, 0, 0):
                    return menu[14];
                case (2, 0, 1):
                    return menu[15];
                case (2, 1, 0):
                    return menu[16];
                case (0, 2, 1):
                    return menu[17];
                case (1, 2, 0):
                    return menu[18];
                case (0, 1, 2):
                    return menu[19];
                case (1, 0, 2):
                    return menu[20];
                case (1, 1, 1):
                    return menu[21];

                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject GetRandomOrder()
    {
        return menu[Random.Range(0, menu.Count)];
    }
}