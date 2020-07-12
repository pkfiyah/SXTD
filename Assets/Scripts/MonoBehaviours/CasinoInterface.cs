using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CasinoInterface : DynamicInterface {

  private int prismiteNodeCount;

  public bool isCasino = false;

  public override void Awake() {
    if (isCasino) {
      prismiteNodeCount = Gameboard.Instance.prismiteNodes;
      inventory.data = new Inventory(inventory, prismiteNodeCount);
    }
    base.Awake();
  }
}
