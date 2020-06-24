using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : Piece {
  void Awake() {
    _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    _isTraversable = false;
  }

  void FixedUpdate() {
    if(_gm.isPlanning) {
      // Do Nothing
      Debug.Log("Wall: Do Nothing");
    } else {
      Debug.Log("Wall: Also Do Nothing");
    }
  }
}
