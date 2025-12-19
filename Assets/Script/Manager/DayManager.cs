using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [field: SerializeField] public static int Day { get; private set; }
    [field: SerializeField] public static DayManager Instance { get; private set; }
    public static event Action onDaySet;

    private void Awake()
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

    private void Start()
    {
        SetDay(1);
    }

    public static void SetDay(int amount)
    {
        IncomeManager.Instance.ResetSalesCount();
        Day = amount;
        CustomerSpawner.Instance.SetCustomerLimit(Day);
        UIManager.Instance.SetDayText(Day.ToString());
    }

    public void GoNextDay()
    {
        UIManager.Instance.DisableSalesResult();

        IncomeManager.Instance.ResetSalesCount();
        Day++;
        CustomerSpawner.Instance.SetCustomerLimit(Day);
        UIManager.Instance.SetDayText(Day.ToString());
    }
}
