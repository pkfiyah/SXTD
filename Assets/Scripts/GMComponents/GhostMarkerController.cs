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
        TDEvents.RequestConstruction.AddListener(SetGhostPiece);
        ghostSprite.enabled = false;
    }

    void OnDisable() {
        TDEvents.CurrencyChange.RemoveListener(IsBroke);
        TDEvents.RequestConstruction.RemoveListener(SetGhostPiece);
    }

    private void IsBroke(int currentBots) {
      if (currentBots <= 0) isBroke = true;
      else isBroke = false;
    }

    void CleanBoard() {
      ghostTileMap.ClearAllTiles();
      isClean = true;
    }

    void SetGhostPiece(GameObject constructablePiece) {
      if (constructablePiece != null) {
        ghostSprite.sprite = constructablePiece.GetComponent<SpriteRenderer>().sprite;
        ghostSprite.enabled = true;
      } else {
        ghostSprite.enabled = false;
      }
    }

    void Update() {
      if (!isClean) this.CleanBoard();
      if(ghostSprite.enabled) {
        if (ghostSprite != null) {
          ghostSprite.sprite = ghostSprite.sprite;
          ghostSprite.transform.position = ghostTileMap.GetCellCenterWorld(ghostTileMap.WorldToCell(MouseData.GetWorldPosition));
          if (!isBroke) ghostSprite.color = GhostMarkerController.GHOST_WHITE;
          else ghostSprite.color = GhostMarkerController.GHOST_RED;
        }
        isClean = false;
      }
    }
}
