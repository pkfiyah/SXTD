using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionManager : MonoBehaviour {

  private GameObject currentPiece;
  private GameObject oldPiece;

  void OnEnable() {
    TDEvents.RequestConstruction.AddListener(PrepareConstruction);
  }

  void OnDisable() {
    TDEvents.RequestConstruction.RemoveListener(PrepareConstruction);
  }

  private void PrepareConstruction(GameObject constructablePiece) {
    if (constructablePiece != null) {
      currentPiece = constructablePiece;
    } else {
      currentPiece = null;
    }
  }
}
