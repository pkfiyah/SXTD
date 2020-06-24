using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicUnit : Piece {

  void FixedUpdate() {
    if(_gm.isPlanning) {
      // Do Nothing
      Debug.Log("BasicUnit: Do Nothing");
    } else {
      Debug.Log("BasicUnit: Also Do Nothing");
    }
  }

}
