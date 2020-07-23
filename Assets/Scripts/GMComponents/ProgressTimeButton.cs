using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTimeButton : MonoBehaviour {

    private readonly string[] buttonAnimationStates  = { "Sunrise", "Morning", "Noon", "Afternoon", "Sundown", "Nighttime" };

    private Animator buttonAnimator;
    private TextMeshProUGUI rerollCount;
    private int maxDaytimeLeft = 12;
    private int lastTime;

    void Awake() {
      buttonAnimator = GetComponentInChildren<Animator>();
      rerollCount = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable() {
      TDEvents.TimeChange.AddListener(OnTimeChange);
      TDEvents.IsNightChange.AddListener(OnNightChange);
    }

    void OnDisable() {
      TDEvents.TimeChange.RemoveListener(OnTimeChange);
      TDEvents.IsNightChange.RemoveListener(OnNightChange);
    }

    private void OnTimeChange(int newTime) {
      rerollCount.text = string.Concat(DaytimeLeft(newTime) / 4);
      if (Mathf.Abs(lastTime - newTime) >= 3 && (newTime >= GameClock.DAYTIME_STARTTIME && newTime <= GameClock.NIGHTTIME_STARTTIME)) {
        buttonAnimator.Play(GetAnimationState(newTime));
        lastTime = newTime;
      }
    }

    private void OnNightChange(bool isNight) {
      if (isNight) {
        buttonAnimator.Play(GetAnimationState(0));
        return;
      }
    }

    private int DaytimeLeft(int currentTime) {
      if (currentTime >= GameClock.DAYTIME_STARTTIME && currentTime <= GameClock.NIGHTTIME_STARTTIME) return GameClock.NIGHTTIME_STARTTIME - GameClock.DAYTIME_STARTTIME - (currentTime - GameClock.DAYTIME_STARTTIME);
      return 0;
    }

    private string GetAnimationState(int currentTime) {
      if (currentTime >= GameClock.DAYTIME_STARTTIME && currentTime <= GameClock.NIGHTTIME_STARTTIME)
      return buttonAnimationStates[(int)Mathf.Floor((maxDaytimeLeft - DaytimeLeft(currentTime)) / 3)];
      return buttonAnimationStates[buttonAnimationStates.Length - 1];
    }
}
