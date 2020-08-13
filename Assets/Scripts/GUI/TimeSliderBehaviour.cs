using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider)) ]
public class TimeSliderBehaviour : MonoBehaviour
{
    private Slider slider;
    void Awake() {
      slider = GetComponent<Slider>();
      slider.value = 0;
    }

    void OnEnable() {
      TDEvents.TimeChange.AddListener(OnTimeChange);
      TDEvents.IsNightChange.AddListener(OnIsNightChange);
    }

    void OnDisable() {
      TDEvents.TimeChange.RemoveListener(OnTimeChange);
      TDEvents.IsNightChange.RemoveListener(OnIsNightChange);
    }

    private void OnTimeChange(int newTime) {
      if (newTime >= GameClock.DAYTIME_STARTTIME && newTime <= GameClock.NIGHTTIME_STARTTIME) {
        slider.value = newTime;
      } else {
        if (newTime > GameClock.NIGHTTIME_STARTTIME && newTime < GameClock.HOURS_IN_DAY) {
          slider.value = newTime - 12;
        } else {
          slider.value = newTime + 6;
        }
      }
    }

    private void OnIsNightChange(bool isNight) {
      slider.value = 6;
    }
}
