using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface {

  public GameObject inventoryPrefab;
  public int X_SPACE_BETWEEN;
  public int X_START;
  public int Y_START;

  public override void createSlots() {
    slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    for (int i = 0; i < inventory.GetSlots.Length; i++) {
      var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
      obj.GetComponent<RectTransform>().localPosition = getPosition(i);

      addEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
      addEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
      addEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
      addEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
      addEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
      inventory.GetSlots[i].slotDisplay = obj;
      slotsOnInterface.Add(obj, inventory.GetSlots[i]);
    }
  }

  private Vector3 getPosition(int i) {
    return new Vector3(X_START + (X_SPACE_BETWEEN * i), Y_START + 0f, 0f);
  }
}
