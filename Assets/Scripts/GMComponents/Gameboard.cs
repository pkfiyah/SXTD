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
    private List<Vector3Int> _spawnPoints;
    private int activeEnemyCount = 0;


    void Awake() {
      Instance = this;
      _spawnPoints = new List<Vector3Int>();
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
          UpdateGameboard(new Vector3Int(i, j, 0), yikes);
        }
      }
      for (int i = 0; i < 1; i++) {
        int randX = Random.Range((int)_levelGen.GetUnstableGround().x, (int)(_levelGen.GetUnstableGround().width + _levelGen.GetUnstableGround().x));
        int randY = Random.Range((int)_levelGen.GetUnstableGround().y, (int)(_levelGen.GetUnstableGround().height + _levelGen.GetUnstableGround().y));
        _spawnPoints.Add(UpdateGameboard(new Vector3Int(randX, randY, 0), Instantiate(spawnPoint, GetWorldPositionFromTilePosition(new Vector3Int(randX, randY, 0)), Quaternion.identity)));
      }
    }

    private void GoGreen() {
      for (int i = 0; i < boardDimensions.x; i++) {
        for (int j = 0; j < boardDimensions.y; j++) {
          SetTileGraphic(groundTilemap, new Vector3Int(i, j, 0), emptyTile);
        }
      }
    }

    // All modifications to the game board happen here
    public Vector3Int UpdateGameboard(Vector3Int tilePosition, GameObject piece) {
      if(!IsOnGameboard(tilePosition)) return tilePosition;
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
      } else {
        activeEnemyCount++;
        piece.GetComponent<EntityPiece>().SetPathToTargetPosition(aStar(tilePosition));
        gp.pieceDestructionDelegate += OnEnemyDestroyed;
      }
      return tilePosition;
    }

    private void OnEnemyDestroyed(GameObject enemyDestroyed) {
      activeEnemyCount--;
      if (!HasEnemiesRemaining()) GameMaster.Instance.EndNighttime();
    }

    private bool HasEnemiesRemaining() {
      for (int i = 0; i < _spawnPoints.Count; i++) {
        Vector3Int currTile = _spawnPoints[i];
        if (_gameboard[currTile.x, currTile.y].GetComponent<WaveBehaviour>().HasWaves) return true;
      }
      if (activeEnemyCount > 0) return true;
      return false;
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
