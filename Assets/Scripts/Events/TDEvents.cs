using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TDEvents {
    public static RunStateChangeEvent BeforeStateChange = new RunStateChangeEvent();
    public static RunStateChangeEvent AfterStateChange = new RunStateChangeEvent();

    public static CurrencyChangeEvent CurrencyChange = new CurrencyChangeEvent();
    public static TimeChangeEvent TimeChange = new TimeChangeEvent();
}

public class RunStateChangeEvent : UnityEvent<State> { }
public class CurrencyChangeEvent : UnityEvent<int> { }
public class TimeChangeEvent : UnityEvent<int> { }
