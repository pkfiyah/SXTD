using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyInterface : MonoBehaviour {
    public TextMeshProUGUI timeValue;
    public TextMeshProUGUI botsValue;

    void OnEnable() {
      TDEvents.CurrencyChange.AddListener(OnCurrencyChange);
      TDEvents.TimeChange.AddListener(OnTimeChange);
    }

    void OnDisable() {
      TDEvents.CurrencyChange.RemoveListener(OnCurrencyChange);
      TDEvents.TimeChange.RemoveListener(OnTimeChange);
    }

    private void OnCurrencyChange(int _currency) {
      botsValue.text = _currency.ToString();
    }

    private void OnTimeChange(int time) {
      timeValue.text = GetTimeDisplay(time);
    }

    public string GetTimeDisplay(int time) {
      string suffix = "AM";
      if (time >= 12 && time <= 23) suffix = "PM";
      int modTime = time % 12;
      if (modTime == 0) modTime = 12;
      if (modTime <= 9) {
        return string.Concat("0", modTime, ":00", suffix);
      } else {
        return string.Concat(modTime, ":00", suffix);
      }
    }
}
