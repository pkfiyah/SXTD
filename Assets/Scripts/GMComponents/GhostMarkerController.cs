using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMarkerController : MonoBehaviour {

    public static Color GHOST_WHITE = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    public Tilemap ghostTileMap;
    public Tilemap entityTileMap;

    private bool isClean = true;

    public void cleanBoard() {
      ghostTileMap.ClearAllTiles();
      isClean = true;
    }

    void FixedUpdate() {
      if (!isClean) this.cleanBoard();
      if(MouseData.activeSelection != null) {
        Tile tileRef = MouseData.activeSelection.GetComponent<Piece>().tile;
        if (tileRef != null) {
          tileRef.color = GhostMarkerController.GHOST_WHITE;
          ghostTileMap.SetTile(MouseData.GetTilePosition, tileRef);
        }
        isClean = false;
      }
    }
}
