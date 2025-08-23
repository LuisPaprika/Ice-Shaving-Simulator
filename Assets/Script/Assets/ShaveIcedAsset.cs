using System.Collections.Generic;
using UnityEngine;

public class ShaveIcedAsset : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> ShavedIcePrefab { get; private set; }

    public GameObject getPrefab(ShavedIceFlavor flavor)
    {
        switch (flavor)
        {
            case ShavedIceFlavor.Strawberry:
                return ShavedIcePrefab[0];
            case ShavedIceFlavor.Chocolate:
                return ShavedIcePrefab[1];
            case ShavedIceFlavor.Milk:
                return ShavedIcePrefab[2];

            default:
                return null;
        }

    }

    public ShavedIceFlavor getRandomFlavor()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                return ShavedIceFlavor.Strawberry;
            case 1:
                return ShavedIceFlavor.Chocolate;
            case 2:
                return ShavedIceFlavor.Milk;

            default:
                return ShavedIceFlavor.Blank;
        }
    }
}
