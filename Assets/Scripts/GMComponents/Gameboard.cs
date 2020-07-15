using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {

    public Vector2Int boardDimensions;
    public Tilemap groundTilemap;
    public Tile emptyTile;
    public GameObject gamepieceTemplate;
    public int prismiteNodes = 3;

    public GameObject emptyPiece;
    public GameObject prismitePiece;
    public GameObject unstableGroundPiece;
    public GameObject hearthPiece;
    public GameObject spawnPoint;

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
      GoGreen();
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          GameObject yikes;
          if (generatedLevel[i, j].type == PieceType.Prismite) {
            yikes = Instantiate(prismitePiece, GetWorldPositionFromTilePosition(new Vector3Int(i, j, 0)), Quaternion.identity);
          } else if (generatedLevel[i, j].type == PieceType.UnstableGround) {
            yikes = Instantiate(unstableGroundPiece, GetWorldPositionFromTilePosition(new Vector3Int(i, j, 0)), Quaternion.identity);
          } else if (generatedLevel[i, j].type == PieceType.Hearth) {
            yikes = Instantiate(hearthPiece, GetWorldPositionFromTilePosition(new Vector3Int(i, j, 0)), Quaternion.identity);
          } else {
            yikes = Instantiate(emptyPiece, GetWorldPositionFromTilePosition(new Vector3Int(i, j, 0)), Quaternion.identity);
          }
          UpdateGameboard(new Vector3Int(i, j, 0), yikes); // Piece database is indexed by type / int type
        }
      }
      for (int i = 0; i < prismiteNodes; i++) {
        int randX = Random.Range((int)_levelGen.GetUnstableGround().x, (int)(_levelGen.GetUnstableGround().width + _levelGen.GetUnstableGround().x));
        int randY = Random.Range((int)_levelGen.GetUnstableGround().y, (int)(_levelGen.GetUnstableGround().height + _levelGen.GetUnstableGround().y));
        UpdateGameboard(new Vector3Int(randX, randY, 0), Instantiate(spawnPoint, GetWorldPositionFromTilePosition(new Vector3Int(randX, randY, 0)), Quaternion.identity)); // Piece database is indexed by type / int type THIS NEEDS TO POINT TO SPAWN PIECE
      }
    }

    // private createGameboardPiece(PieceObject piece) {
    //   GameObject go = Instantiate(piece, new Vector3(0f, 0f, 0f), Quaternion.identity);
    // }

    private void GoGreen() {
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          SetTileGraphic(groundTilemap, new Vector3Int(i, j, 0), emptyTile);
        }
      }
    }

    // All modifications to the game board happen here
    public void UpdateGameboard(Vector3Int tilePosition, GameObject piece) {
      if(!IsOnGameboard(tilePosition)) return;

      GameboardPiece gp = piece.GetComponent<GameboardPiece>();
      piece.transform.parent = this.transform;
      gp.piece.parent = this;
      piece.transform.position = GetWorldPositionFromTilePosition(tilePosition);
      // Sets the tile portion of the graphic to the board
      if (piece.GetComponent<SpriteRenderer>() == null && gp.piece.tile != null) {
        if (gp.piece.data.type == PieceType.UnstableGround || gp.piece.data.type == PieceType.GroundConstruction || gp.piece.data.type == PieceType.Empty ) {
          SetTileGraphic(groundTilemap, tilePosition, gp.piece.tile);
        }
      }

      if (gp.piece.data.type == PieceType.Hearth) {
        _hearthTileRef = new Vector2Int(tilePosition.x, tilePosition.y);
      }

      if (gp.piece.data.type != PieceType.Entity) {
        Destroy(_gameboard[tilePosition.x, tilePosition.y]);
        _gameboard[tilePosition.x, tilePosition.y] = piece;
      }
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
      Vector3Int ret = groundTilemap.WorldToCell(worldPosition);
      ret.z = 0;
      return ret;
    }

    public Vector3 GetWorldPositionFromTilePosition(Vector3Int tilePosition) {
      Vector3 ret = groundTilemap.GetCellCenterWorld(tilePosition);
      ret.z = 0f;
      return ret;
    }

    public void SetTileGraphic(Tilemap _tilemap, Vector3Int tilePosition, Tile tile) {
      tile.color = Color.white;
      _tilemap.SetTile(tilePosition, tile);
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
  public static GameObject activeSelection;
  public static Vector3 GetWorldPosition { get { Vector3 normalZmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); normalZmousePos.z = 0; return normalZmousePos; }}
}
