using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMarkerController : MonoBehaviour {

    public static Color GHOST_WHITE = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public static Color GHOST_RED = new Color(1.0f, 0f, 0f, 0.5f);
    public static Color RANGE_YELLOW = new Color(1f, 0.8f, 0f, 0.5f);

    public TileBase attackRangeTile;
    public TileBase placementRangeTile;
    public TileBase unselectableTile;

    public Tilemap ghostTileMap;
    public SpriteRenderer ghostSprite;

    private GameObject selection;

    private bool isClean = true;
    private bool isBroke = false;
    private bool repositioning = false;

    void OnEnable() {
        ghostTileMap.color = GhostMarkerController.GHOST_WHITE;
        TDEvents.CurrencyChange.AddListener(IsBroke);
        TDEvents.RequestConstruction.AddListener(SetGhostPiece);
        TDEvents.RequestReposition.AddListener(SetGhostRepositionInfo);
        ghostSprite.enabled = false;
    }

    void OnDisable() {
        TDEvents.CurrencyChange.RemoveListener(IsBroke);
        TDEvents.RequestConstruction.RemoveListener(SetGhostPiece);
        TDEvents.RequestReposition.RemoveListener(SetGhostRepositionInfo);
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
        selection = constructablePiece;
        repositioning = true;
        ghostSprite.sprite = constructablePiece.GetComponent<SpriteRenderer>().sprite;
        ghostSprite.enabled = true;
      } else {
        ghostSprite.enabled = false;
        repositioning = false;
        selection = null;
      }
    }

    void SetGhostRepositionInfo(GameObject constructablePiece) {
      if (constructablePiece != null) {
        selection = constructablePiece;
        repositioning = true;
      } else {
        repositioning = false;
        selection = null;
      }
    }

    void Update() {
      if (!isClean) this.CleanBoard();
      if (repositioning) {
        // Draw tiles over blocked areas always
        DrawTiles(unselectableTile,  Gameboard.Instance.GetBlockedArea());

        int range = selection.GetComponent<GameboardPiece>().piece.data.baseRange;
        if (repositioning && !ghostSprite.enabled) {
          Vector3Int tilePos = ghostTileMap.WorldToCell(MouseData.GetWorldPosition);
          Vector3Int min = new Vector3Int(tilePos.x - range, tilePos.y - range, 0);
          Vector3Int max = new Vector3Int(tilePos.x + range, tilePos.y + range, 0);
          Vector3Int piecePos = selection.GetComponent<GameboardPiece>().GetTilePosition();
          Vector3Int pieceMin = new Vector3Int(piecePos.x - range, piecePos.y - range, 0);
          Vector3Int pieceMax = new Vector3Int(piecePos.x + range, piecePos.y + range, 0);
          DrawTiles(placementRangeTile, pieceMin, pieceMax);
          DrawTiles(attackRangeTile, min, max);
        } else {
          Vector3Int tilePos = ghostTileMap.WorldToCell(MouseData.GetWorldPosition);
          Vector3Int min = new Vector3Int(tilePos.x - range, tilePos.y - range, 0);
          Vector3Int max = new Vector3Int(tilePos.x + range, tilePos.y + range, 0);
          if (selection.GetComponent<EntitySpawnerPiece>() != null) {
            DrawTiles(placementRangeTile, min, max);
          } else {
            DrawTiles(attackRangeTile, min, max);
          }
        }
        isClean = false;
      }

      if(ghostSprite.enabled) {
        if (ghostSprite != null) {
          ghostSprite.transform.position = ghostTileMap.GetCellCenterWorld(ghostTileMap.WorldToCell(MouseData.GetWorldPosition));
          if (!isBroke) ghostSprite.color = GhostMarkerController.GHOST_WHITE;
          else ghostSprite.color = GhostMarkerController.GHOST_RED;
        }
        isClean = false;
      }
    }

    void DrawTiles(TileBase tile, Vector3Int min, Vector3Int max) {
      if (min == max) {
        ghostTileMap.SetTile(new Vector3Int(min.x, min.y, 0), tile);
      } else {
        for (int i = min.x; i <= max.x; i++) {
          for (int j = min.y; j <= max.y; j++) {
            ghostTileMap.SetTile(new Vector3Int(i, j, 0), tile);
          }
        }
      }
    }

    void DrawTiles(TileBase tile, List<Vector3Int> tilePositionList) {
      foreach(Vector3Int pos in tilePositionList) {
        ghostTileMap.SetTile(pos, tile);
      }
    }
}
