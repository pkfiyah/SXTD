using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock {
    private int time = 0;
    public GameClock (int startTime) {
      time = startTime - 1;
      Tick();
    }

    public void Tick() {
      TDEvents.TimeChange.Invoke(++time);
      if (time > 23) time = time % 12;
    }

    public void Tick(int numTicks) {
      TDEvents.TimeChange.Invoke(time += numTicks);
      if (time > 23) time = time % 12;
    }

    public bool IsDaytime() {
      if (time >= 6 && time < 18) return true;
      return false;
    }

    public int GetTime() {
      return time;
    }
}
