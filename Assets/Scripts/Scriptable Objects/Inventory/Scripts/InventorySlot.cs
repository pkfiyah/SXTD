using System.Collections;
using UnityEngine;

public delegate void SlotUpdated(InventorySlot slot);

[System.Serializable]
public class InventorySlot {

  [System.NonSerialized]
  public UserInterface parent;
  [System.NonSerialized]
  public GameObject slotDisplay;
  [System.NonSerialized]
  public SlotUpdated OnBeforeUpdate;
  [System.NonSerialized]
  public SlotUpdated OnAfterUpdate;

  public Prismite prismite;

  public PrismiteObject PrismiteObject {
    get {
      if (prismite.id >= 0) { // This is the getter for the item the inventory slot is holding [Ensures it's a valid item in the database]
         Debug.Log("prismiteId: " + prismite.id);
         Debug.Log("parent.inventory.database.GetPrismite[" + parent.inventory.database.CheckDb());
         return parent.inventory.database.GetPrismite[prismite.id];
      }
      return null;
    }
  }

  public InventorySlot() {
    UpdateSlot(new Prismite());
  }

  public InventorySlot(Prismite _prismite) {
    UpdateSlot(_prismite);
  }

  public void UpdateSlot(Prismite _prismite) {
    if (OnBeforeUpdate != null) {
      OnBeforeUpdate.Invoke(this);
    }

    prismite = _prismite;

    if (OnAfterUpdate != null) {
      OnAfterUpdate.Invoke(this);
    }
  }

  public void RemoveItem() {
    UpdateSlot(new Prismite());
  }
}
