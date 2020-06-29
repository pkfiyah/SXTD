using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class EmptyTile : MonoBehaviour {
    public PieceObject emptyTile;
    void Awake() {
      // if (Random.Range(0, 2) > 0) {
      //   this.tile = emptyTiles[Random.Range(0, emptyTiles.Length - 1)];
      // } else {
      //   this.tile = emptyTiles[emptyTiles.Length - 1];
      // }
    }
    // void OnGUI() {
    //   Vector3 worldPos = this.GetWorldPosition();
    //   Vector3Int tilePos = this.GetTilePosition();
    //   Handles.Label(worldPos, "(" + tilePos.x + ", " + tilePos.y + ")");
    // }
}
