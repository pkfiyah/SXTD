using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface {

  public GameObject[] slots;

  public override void createSlots() {
    slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    for (int i = 0; i < inventory.GetSlots.Length; i++) {
      var obj = slots[i];

      addEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
      addEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
      addEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
      addEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
      addEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
      inventory.GetSlots[i].slotDisplay = obj;
      slotsOnInterface.Add(obj, inventory.GetSlots[i]);
    }
  }
}
