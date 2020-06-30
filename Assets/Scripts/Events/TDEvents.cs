using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TDEvents {
    public static RunStateChangeEvent BeforeStateChange = new RunStateChangeEvent();
    public static RunStateChangeEvent AfterStateChange = new RunStateChangeEvent();
}

public class RunStateChangeEvent : UnityEvent<State> { }
