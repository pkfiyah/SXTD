using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlottableGameboardPiece : GameboardPiece {

    private StaticInterface ui;

    void Awake() {
        ui = GetComponentInChildren<StaticInterface>();
    }

    void FixedUpdate() {
      if (MouseData.activeSelection != this) {
        ui.Disappear();
      }
    }

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        // Select this spot
        MouseData.activeSelection = this;
        ui.Reappear();
      } else {
        MouseData.activeSelection = null;
      }
    }
}
