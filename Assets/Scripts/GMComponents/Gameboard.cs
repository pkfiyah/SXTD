using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {

    public PieceDatabaseObject pieceDatabase;
    public Vector2Int boardDimensions;
    public Tilemap entityTilemap;
    public GameObject gameboardPieceTemplatePrefab;
    public GameObject spawnPointPrefab;
    public GameObject enemyPrefab;

    public static Gameboard Instance { get; private set; }

    private Vector2Int _hearthTileRef;
    private Pathfinding _pathfinder;
    private GameObject[,] _gameboard;
    private LevelGenerator _levelGen;

    void Awake() {
      Instance = this;
      _gameboard = new GameObject[boardDimensions.x, boardDimensions.y];
      _pathfinder = new Pathfinding(boardDimensions.x, boardDimensions.y);
      _levelGen = new LevelGenerator(boardDimensions.x, boardDimensions.y);
      RealizeGameBoard();
    }

    // Instantiate all GameboardPieces from Pieces
    private void RealizeGameBoard() {
      Piece[,] generatedLevel = _levelGen.GetLevel();
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          UpdateGameboard(new Vector3Int(i, j, 0), pieceDatabase.GetPiece[(int)generatedLevel[i, j].type]); // Piece database is indexed by type / int type
        }
      }
      for (int i = 0; i < 3; i++) {
        int randX = Random.Range((int)_levelGen.GetUnstableGround().x, (int)(_levelGen.GetUnstableGround().width + _levelGen.GetUnstableGround().x));
        int randY = Random.Range((int)_levelGen.GetUnstableGround().y, (int)(_levelGen.GetUnstableGround().height + _levelGen.GetUnstableGround().y));
        UpdateGameboard(new Vector3Int(randX, randY, 0), pieceDatabase.GetPiece[(int)PieceType.SpawnPoint]); // Piece database is indexed by type / int type THIS NEEDS TO POINT TO SPAWN PIECE
      }
    }

    private GameObject GetPrefabFromType (PieceObject piece) {
      switch(piece.data.type) {
        case PieceType.SpawnPoint:
          return spawnPointPrefab;
        case PieceType.Entity:
          return enemyPrefab;
        default:
          return gameboardPieceTemplatePrefab;
      }
    }

    // All modifications to the game board happen here
    public GameObject UpdateGameboard(Vector3Int tilePosition, PieceObject piece) {
      if(!IsOnGameboard(tilePosition)) return null;

      GameObject newGameboardPiece = Instantiate(GetPrefabFromType(piece), GetWorldPositionFromTilePosition(tilePosition), Quaternion.identity);
      GameboardPiece gp = newGameboardPiece.GetComponent<GameboardPiece>();
      gp.piece = piece;
      newGameboardPiece.transform.parent = this.transform;
      gp.piece.parent = this;

      // Sets the tile portion of the graphic to the board
      if (newGameboardPiece.GetComponent<SpriteRenderer>() == null && piece.tile != null) {
        SetTileGraphic(tilePosition, piece.tile);
      }

      if (_gameboard[tilePosition.x, tilePosition.y] != null) Destroy(_gameboard[tilePosition.x, tilePosition.y]);
      if (piece.data.type == PieceType.Hearth) {
        _hearthTileRef = new Vector2Int(tilePosition.x, tilePosition.y);
      }
      
      if (piece.data.type != PieceType.Entity) { // Entities dont "occupy" a space
        _gameboard[tilePosition.x, tilePosition.y] = newGameboardPiece;
      }
      return newGameboardPiece;
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
      List<PathNode> path = _pathfinder.findPath(startTilePos.x, startTilePos.y, _hearthTileRef.x, _hearthTileRef.y);
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
