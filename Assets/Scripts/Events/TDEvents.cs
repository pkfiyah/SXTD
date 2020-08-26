using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TDEvents {
    public static CurrencyChangeEvent CurrencyChange = new CurrencyChangeEvent();
    public static TimeChangeEvent TimeChange = new TimeChangeEvent();
    public static DayNightChangeEvent IsNightChange = new DayNightChangeEvent();
    public static GameOverEvent GameOver = new GameOverEvent();
    public static SelectionChangedEvent SelectionChanged = new SelectionChangedEvent();

    public static RequestDroneEvent RequestDrone = new RequestDroneEvent();
    public static RequestConstructionEvent RequestConstruction = new RequestConstructionEvent();
    public static RequestRepositionEvent RequestReposition = new RequestRepositionEvent();
}

public class CurrencyChangeEvent : UnityEvent<int> { }
public class TimeChangeEvent : UnityEvent<int> { }
public class DayNightChangeEvent: UnityEvent<bool> { }
public class GameOverEvent: UnityEvent { }
public class SelectionChangedEvent: UnityEvent<GameObject> { }

public class RequestDroneEvent: UnityEvent<Vector3Int> { }
public class RequestConstructionEvent: UnityEvent<GameObject> { }
public class RequestRepositionEvent: UnityEvent<GameObject> { }
