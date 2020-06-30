using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        MouseData.activeSelection = this;
      } else {
        MouseData.activeSelection = null;
      }
    }

    public void OnBeforeStateChange() {
      // Do this
    }

    public void OnAfterStateChange() {
      // Do this
    }
}
