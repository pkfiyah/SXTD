using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTilemapController : MonoBehaviour {
    public Tilemap groundTileMap;
    public Tile groundTile;

    void Awake() {
        Vector2Int boardDims = Gameboard.Instance.boardDimensions;
        Vector2Int dims = new Vector2Int(boardDims.x, boardDims.y);
        for (int i = 0; i < dims.x; i++) {
          for (int j = 0; j < dims.y; j++) {
            groundTileMap.SetTile(new Vector3Int(i, j, 0), groundTile);
          }
        }
    }
}
