using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [field: SerializeField] public static int Day { get; private set; }
    private DayManager instance;
    public static event Action<int> onDaySet;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
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
