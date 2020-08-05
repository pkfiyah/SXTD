using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour {
  void Update() {
    Debug.Log("HoverTarget: " + MouseData.hoverTarget);
    
  }
}
