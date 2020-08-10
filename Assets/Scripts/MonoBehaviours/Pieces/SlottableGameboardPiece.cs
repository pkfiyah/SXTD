using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlottableGameboardPiece : GameboardPiece {

    protected StaticInterface ui;
    // public InventoryObject inventory;
    private Transform uiPositionHold;
    private Transform uiPosition;

    // public virtual void Awake() {
    //   // inventory = Instantiate(inventory); // Must make copy of initial inventory
    //   // base.Awake();
    //   ui = GetComponentInChildren<StaticInterface>();
    //   // ui.Disappear();
    // }

    void FixedUpdate() {
      if (MouseData.activeSelection != this.gameObject) {
        ui.Disappear();
      }
    }

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        // Select this spot
        MouseData.activeSelection = this.gameObject;
        // uiPosition.position = Camera.main.WorldToScreenPoint(transform.position);
        ui.Reappear(GetPosition());
      } else {
        MouseData.activeSelection = null;
      }
    }

    private void OnBeforeSlotUpdate(InventorySlot slot) {
      if (slot.prismite != null && slot.prismite.id >= 0) {
        for (int i = 0; i < slot.prismite.buffs.Length; i++) {
          Debug.Log("Removing Modifier: " + slot.prismite.buffs[i].value);
          damage.RemoveModifier(slot.prismite.buffs[i]);
        }
      }
    }

    private void OnAfterSlotUpdate(InventorySlot slot) {
      for (int i = 0; i < slot.prismite.buffs.Length; i++) {
        Debug.Log("Adding Modifier: " + slot.prismite.buffs[i].value);
        damage.AddModifier(slot.prismite.buffs[i]);
      }
    }

    private void OnDisable() {
      for (int i = 0; i < ui.inventory.GetSlots.Length; i++) {
        ui.inventory.GetSlots[i].OnBeforeUpdate -= OnBeforeSlotUpdate;
        ui.inventory.GetSlots[i].OnAfterUpdate -= OnAfterSlotUpdate;
      }
    }

    // must add listeners to slots after the UI initializes for the piece
    public void ListenToSlots() {
      for (int i = 0; i < ui.inventory.GetSlots.Length; i++) {
        ui.inventory.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
        ui.inventory.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
      }
    }

    public override void OnAfterPlaced() {
      TilePosition = Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position);
    }
}
