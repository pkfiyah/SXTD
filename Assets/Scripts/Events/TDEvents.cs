using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TDEvents {
    public static CurrencyChangeEvent CurrencyChange = new CurrencyChangeEvent();
    public static TimeChangeEvent TimeChange = new TimeChangeEvent();
    public static DayNightChangeEvent IsNightChange = new DayNightChangeEvent();
}

public class CurrencyChangeEvent : UnityEvent<int> { }
public class TimeChangeEvent : UnityEvent<int> { }
public class DayNightChangeEvent: UnityEvent<bool> { }
