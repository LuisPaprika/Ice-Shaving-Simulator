using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    [field: SerializeField] public static IncomeManager Instance { get; private set; }
    [field: SerializeField] public List<ScriptableObject> Sales { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ResetSalesCount()
    {
        foreach(ScriptableObject so in Sales)
        {
            MenuSO menuSO = so as MenuSO;
            if(menuSO != null)
            {
                menuSO.sales = 0;
            }
        }
    }
    public void AddSalesCount(string menu, int count)
    {
        foreach(ScriptableObject so in Sales)
        {
            MenuSO menuSO = so as MenuSO;
            if(menuSO != null)
            {
                if(menuSO.menuName == menu)
                {
                    menuSO.sales += count;
                }
            }
        }
    }


}
