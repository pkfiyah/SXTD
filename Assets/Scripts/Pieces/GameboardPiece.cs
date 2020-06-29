using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// NOTES--- May need to Setup up mini canvases with reference to Camera for proper placement.
/// maybe don't need sub canvas
// Test multiple inventories and towers --> Doesnt currently work, Make a Non-Scriptable Object version of Inventories/InventorySlots for Tower Piece
//


public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        // Select this spot
        Debug.Log("Selected Piece");
        MouseData.activeSelection = this;
      } else {
        Debug.Log("Unselected Piece");
        MouseData.activeSelection = null;
      }
    }
}
