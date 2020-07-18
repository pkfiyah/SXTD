using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CasinoInterface : DynamicInterface {

  private int prismiteNodeCount;

  public bool isCasino = false;

  public override void Awake() {
    if (isCasino) {
      prismiteNodeCount = 3;
      inventory.data = new Inventory(inventory, prismiteNodeCount);
    }
    base.Awake();
  }

  public override void OnDragEnd(GameObject obj) {
    Destroy(MouseData.tempItemBeingDragged);
    if (MouseData.interfaceMouseIsOver == null) {
      return;
    }
    if (MouseData.interfaceMouseIsOver.canAcceptItems && MouseData.slotHoveredOver && isCasino && GameMaster.Instance.MakePurchase(1))  {// nater will need price here
      InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
      inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
    }
  }
}
