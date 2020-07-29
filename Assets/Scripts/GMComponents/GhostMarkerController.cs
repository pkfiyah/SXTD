using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMarkerController : MonoBehaviour {

    public static Color GHOST_WHITE = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public static Color GHOST_RED = new Color(1.0f, 0f, 0f, 0.5f);

    public Tilemap ghostTileMap;
    public SpriteRenderer ghostSprite;

    private bool isClean = true;
    private bool isBroke = false;


    void OnEnable() {
        TDEvents.CurrencyChange.AddListener(IsBroke);
    }

    void OnDisable() {
        TDEvents.CurrencyChange.RemoveListener(IsBroke);
    }

    private void IsBroke(int currentBots) {
      if (currentBots <= 0) isBroke = true;
      else isBroke = false;
    }

    void CleanBoard() {
      ghostTileMap.ClearAllTiles();
      isClean = true;
    }

    void Update() {
      if (!isClean) this.CleanBoard();
      if(MouseData.tempPieceBeingDragged != null) {
        Tile tileRef = MouseData.tempPieceBeingDragged.GetComponent<GameboardPiece>().piece.tile;
        if (tileRef != null) {
          if (!isBroke) tileRef.color = GhostMarkerController.GHOST_WHITE;
          else tileRef.color = GhostMarkerController.GHOST_RED;
          ghostTileMap.SetTile(ghostTileMap.WorldToCell(MouseData.GetWorldPosition), tileRef);
        } else {
          ghostSprite.sprite = MouseData.tempPieceBeingDragged.GetComponent<SpriteRenderer>().sprite;
          ghostSprite.transform.position = MouseData.tempPieceBeingDragged.transform.position;
          if (!isBroke) ghostSprite.color = GhostMarkerController.GHOST_WHITE;
          else ghostSprite.color = GhostMarkerController.GHOST_RED;
        }

        isClean = false;
      }
    }
}
