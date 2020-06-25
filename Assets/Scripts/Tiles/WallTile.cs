using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : Piece {
  void FixedUpdate() {
    if(GameMaster.Instance.isPlanning) {
      // Do Nothing
      Debug.Log("Wall: Do Nothing");
    } else {
      Debug.Log("Wall: Also Do Nothing");
    }
  }
}
