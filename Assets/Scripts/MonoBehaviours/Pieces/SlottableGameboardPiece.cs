using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlottableGameboardPiece : GameboardPiece {

    private StaticInterface ui;
    public InventoryObject inventory;

    void Awake() {
        ui = GetComponentInChildren<StaticInterface>();
        // inventory = Instantiate(inventory); // Must make copy of initial inventory
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
        MouseData.activeSelection = this.gameObject;
        ui.Reappear();
      } else {
        MouseData.activeSelection = null;
      }
    }
}
