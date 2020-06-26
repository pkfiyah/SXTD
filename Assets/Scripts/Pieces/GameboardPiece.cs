using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;

    void OnMouseDown() {
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        // Select this spot
        MouseData.activeSelection = piece;
      } else {
        MouseData.activeSelection = null;
      }
    }
}
