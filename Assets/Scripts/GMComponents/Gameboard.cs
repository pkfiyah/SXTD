using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {

    public Vector2Int boardDimensions;
    public Tilemap entityTilemap;
    public PieceObject emptyTile;
    public GameObject tileTemplate;
    public static Gameboard Instance { get; private set; }

    private Vector2Int hearthTileRef;
    private Pathfinding _pathfinder;
    private GameObject[,] _gameboard;

    void Awake() {
      Instance = this;
      _gameboard = new GameObject[boardDimensions.x, boardDimensions.y];
      _pathfinder = new Pathfinding(boardDimensions.x, boardDimensions.y);

      // Construct Empty Game Board
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          UpdateGameboard(new Vector3Int(i, j, 0), tileTemplate);
        }
      }
    }

    // All modifications to the game board happen here
    public GameObject UpdateGameboard(Vector3Int tilePosition, GameObject piece) {
      if(!IsOnGameboard(tilePosition)) return null;

      GameObject newPiece = Instantiate(piece, GetWorldPositionFromTilePosition(tilePosition), Quaternion.identity);
      newPiece.transform.parent = this.transform;

      PieceObject pieceObj = newPiece.GetComponent<GameboardPiece>().piece;
      pieceObj.parent = this;

      // Sets the tile portion of the graphic to the board
      if (newPiece.GetComponent<SpriteRenderer>() == null && pieceObj.tile != null) {
        SetTileGraphic(tilePosition, pieceObj.tile);
      }

      if (_gameboard[tilePosition.x, tilePosition.y] != null) Destroy(_gameboard[tilePosition.x, tilePosition.y]);
      if (pieceObj.data.type == PieceType.Hearth) {
        hearthTileRef = new Vector2Int(tilePosition.x, tilePosition.y);
      }

      _gameboard[tilePosition.x, tilePosition.y] = newPiece;
      return newPiece;
    }

    private bool IsOnGameboard(Vector3Int tileCoords) {
      if (tileCoords.x < boardDimensions.x && tileCoords.x >= 0 && tileCoords.y < boardDimensions.y && tileCoords.y >= 0) {
        return true;
      } else {
        return false;
      }
    }

    public GameObject GetPieceAtTile(Vector3Int tilePosition) {
      return _gameboard[tilePosition.x, tilePosition.y];
    }

    public Vector3Int GetTilePositionFromWorldPosition(Vector3 worldPosition) {
      Vector3Int ret = entityTilemap.WorldToCell(worldPosition);
      ret.z = 0;
      return ret;
    }

    public Vector3 GetWorldPositionFromTilePosition(Vector3Int tilePosition) {
      Vector3 ret = entityTilemap.GetCellCenterWorld(tilePosition);
      ret.z = 0f;
      return ret;
    }

    public void SetTileGraphic(Vector3Int tilePosition, Tile tile) {
      tile.color = Color.white;
      entityTilemap.SetTile(tilePosition, tile);
    }

    public List<Vector3Int> aStar(Vector3Int startTilePos) {
      Piece[,] boardRef = new Piece[boardDimensions.x, boardDimensions.y];
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          boardRef[i, j] = _gameboard[i, j].GetComponent<GameboardPiece>().piece.CreatePiece();
        }
      }

      _pathfinder.parseGameBoard(boardRef);
      List<PathNode> path = _pathfinder.findPath(startTilePos.x, startTilePos.y, hearthTileRef.x, hearthTileRef.y);
      List<Vector3Int> convertedPath = new List<Vector3Int>();
      foreach (PathNode node in path) {
        convertedPath.Add(new Vector3Int(node.getX(), node.getY(), 0));
      }
      return convertedPath;
    }
}



public static class MouseData {
  public static GameObject tempItemBeingDragged;
  public static GameObject tempPieceBeingDragged;
  public static GameObject slotHoveredOver;
  public static Piece tileHoveredOver;
  public static UserInterface interfaceMouseIsOver;
  public static GameboardPiece activeSelection;
  public static Vector3 GetWorldPosition { get { Vector3 normalZmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); normalZmousePos.z = 0; return normalZmousePos; }}
}
