using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;

    private State currentState = State.Planning;
    private SpriteRenderer rend;

    void Awake() {
      TDEvents.AfterStateChange.AddListener(OnAfterStateChange);
      if (GetComponent<SpriteRenderer>() != null) {
        rend = GetComponent<SpriteRenderer>();
        if (piece.data.type == PieceType.GroundConstruction) {
          rend.sortingOrder = 1;
        }
      }
    }

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        MouseData.activeSelection = this.gameObject;
      } else {
        MouseData.activeSelection = null;
      }
    }

    public void OnBeforeStateChange() {
      // Do this
    }

    public void OnAfterStateChange(State state) {
      currentState = state;
    }
}
