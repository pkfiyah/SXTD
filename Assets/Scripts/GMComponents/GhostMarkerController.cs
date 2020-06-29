using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMarkerController : MonoBehaviour {

    public static Color GHOST_WHITE = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    public Tilemap ghostTileMap;

    private bool isClean = true;

    public void cleanBoard() {
      ghostTileMap.ClearAllTiles();
      isClean = true;
    }

    void FixedUpdate() {
      if (!isClean) this.cleanBoard();
      if(MouseData.tempPieceBeingDragged != null) {
        Tile tileRef = MouseData.tempPieceBeingDragged.GetComponent<GameboardPiece>().piece.tile;
        if (tileRef != null) {
          tileRef.color = GhostMarkerController.GHOST_WHITE;
          ghostTileMap.SetTile(ghostTileMap.WorldToCell(MouseData.GetWorldPosition), tileRef);
        }
        isClean = false;
      }
    }
}
