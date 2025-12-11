using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [field: SerializeField] public static int Day { get; private set; }
    [field: SerializeField] public static DayManager Instance { get; private set; }
    public static event Action<int> onDaySet;

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
        Day = amount;
        onDaySet.Invoke(Day);
        UIManager.Instance.SetDayText(Day.ToString());
    }
}
