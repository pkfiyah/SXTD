using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using DataStructures.PriorityQueue;

// 24 hour clock, that holds data in reference to day/night cycle
// Day is 6AM to 6PM
public class GameClock {

    public bool IsDaytime { get {
        if (time >= DAYTIME_STARTTIME && time <= NIGHTTIME_STARTTIME && !HasNightStarted) return true;
        return false;
      }
    }

    public static bool HasNightStarted = false;
    public static int ACTIVE_START_DELAY_TIME = 5;
    public static int SECONDS_IN_HOUR = 5;
    public static int NIGHTTIME_STARTTIME = 18;
    public static int DAYTIME_STARTTIME = 6;
    public static int HOURS_IN_DAY = 24;

    private int time = 0;

    public GameClock (int startTime) {
      time = startTime - 1;
      Tick();
    }

    public void Tick() {
      Tick(1);
    }

    public bool Tick(int numTicks) {
      if (!HasNightStarted && time + numTicks > NIGHTTIME_STARTTIME) return false; // Do not tick past 6PM until flag is given
      TDEvents.TimeChange.Invoke(time += numTicks);
      if (time >= HOURS_IN_DAY) time = time % HOURS_IN_DAY;
      if (time == DAYTIME_STARTTIME && HasNightStarted) {
        HasNightStarted = false;
        Debug.Log("Firing Daytime Start");
        TDEvents.IsNightChange.Invoke(false);
      }
      return true;
    }

    public void StartNighttime() {
      Debug.Log("Firing Nighttime Start");
      HasNightStarted = true;
      TDEvents.IsNightChange.Invoke(true);
    }

    public int GetTime() {
      return time;
    }
}
