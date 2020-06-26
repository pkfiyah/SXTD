using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlottedPiece  {
    //
    // [System.NonSerialized]
    // public UserInterface inventoryUi;
    // [System.NonSerialized]
    // public InventoryObject inventory;
    //
    // private CanvasGroup cgroup;
    //
    // void Awake() {
    //   cgroup = gameObject.GetComponentInChildren<CanvasGroup>();
    //   // inventory = new Inventory();
    //   inventoryUi = gameObject.GetComponent<UserInterface>();
    //   // inventory = inventoryUi.inventory;
    // }

    public void OnMouseDown() {
      Debug.Log("Clicked me");
      // if (cgroup.blocksRaycasts) {
      //   cgroup.alpha = 0f;
      //   cgroup.blocksRaycasts = false;
      // } else {
      //   cgroup.alpha = 1f;
      //   cgroup.blocksRaycasts = true;
      // }
    }
}
