using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider slider;

    public void setMaxHealth(float health) {
      slider.maxValue  = health;
      slider.value = health;
    }

    public void setHealth(float health) {
        slider.value = health;
    }

    public void setActive(bool isActive) {
      gameObject.SetActive(isActive);
    }

    public bool getActive() {
      return gameObject.activeInHierarchy;
    }

    public float getHealth(){
      return slider.value;
    }
}
