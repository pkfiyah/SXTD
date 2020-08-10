using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthPiece : GameboardPiece {
    void OnDestroy() {
      TDEvents.GameOver.Invoke();
    }

    public override void OnAfterPlaced() {

    }
}
