using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthTile : Piece {
  void Awake() {
    _health = 200.0f;
    _armour = 2.0f;
    _res = 15.0f;

    gameObject.tag = "HearthTile";
    _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    _isTraversable = true;
  }

  void FixedUpdate() {
    if(_gm.isPlanning) {
      // Do Nothing
      Debug.Log("Hearth: Do Nothing");
    } else {
      Debug.Log("Hearth: Also Do Nothing HP: " + _health);
    }
  }

  void OnMouseUp() {
    Vector3Int mouseTilePos = _gm.getTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    mouseTilePos.z = 0;
    if (isFollowingCursor && _gm.isOnGameboard(mouseTilePos)) {
      _gm.setHearthTile(mouseTilePos, this.gameObject);
    }
  }

  public bool isHearth() {
    return true;
  }
}
