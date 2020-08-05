using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {

    public StageObject stageParams;
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

    private Vector3Int _hearthTileRef;
    private Pathfinding _pathfinder;
    private GameObject[,] _gameboard;
    private LevelGenerator _levelGen;
    private List<Vector3Int> _spawnPoints;
    private int activeEnemyCount = 0;
    private int currentNight = 0;


    void Awake() {
      Instance = this;
      if (stageParams == null) {
        Debug.LogError("StageObject required by Gameboard to generate stage", stageParams);
      }
      _spawnPoints = new List<Vector3Int>();
      _gameboard = new GameObject[stageParams.StageWidth, stageParams.StageLength];
      _pathfinder = new Pathfinding(stageParams.StageWidth, stageParams.StageLength);
      _levelGen = new LevelGenerator(stageParams);
      RealizeGameBoard();
    }

    private void OnNightEnd() {
      if (currentNight >= stageParams.data.nightsInStage) return;
      for (int i = 0; i < _spawnPoints.Count; i++) {
        Vector3Int currTile = _spawnPoints[i];
        _gameboard[currTile.x, currTile.y].GetComponent<WaveBehaviour>().PrepNextNight(stageParams.SpawnPointWaves[currentNight]);
      }
      currentNight++;
    }

    // Instantiate all GameboardPieces from Pieces
    private void RealizeGameBoard() {
      Piece[,] generatedLevel = _levelGen.GetLevel();
      GoGreen();
      for (int i = 0; i < stageParams.StageWidth; i++) {
        for (int j = 0; j < stageParams.StageLength; j++) {
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
        Vector3Int spawnPointPosition = _levelGen.GetValidSpawnPointPosition();
        _spawnPoints.Add(UpdateGameboard(spawnPointPosition, Instantiate(spawnPoint, GetWorldPositionFromTilePosition(spawnPointPosition), Quaternion.identity)));
        OnNightEnd();
      }
    }

    private void GoGreen() {
      for (int i = 0; i < stageParams.StageWidth; i++) {
        for (int j = 0; j < stageParams.StageLength; j++) {
          SetTileGraphic(groundTilemap, new Vector3Int(i, j, 0), emptyTile);
        }
      }
    }

    /**
    * All modifications to the game board happen here
    * Input is a position and an already instantiated GameObject
    * This ensures pieces are displayed and placed in a consistant way
    **/
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
        _hearthTileRef = new Vector3Int(tilePosition.x, tilePosition.y, 0);
      }

      if (gp.piece.data.type != PieceType.Entity) {
        Destroy(_gameboard[tilePosition.x, tilePosition.y]);
        _gameboard[tilePosition.x, tilePosition.y] = piece;
      } else {
        // activeEnemyCount++;
        // piece.GetComponent<EntityPiece>().SetPathToTargetPosition(aStar(tilePosition));
        // gp.EntityDestructionEvent += OnEnemyDestroyed;
      }
      return tilePosition;
    }

    public void AddEntity(Vector3Int tilePosition, GameObject entity) {
      entity.transform.position = GetWorldPositionFromTilePosition(tilePosition);
      entity.GetComponent<GameboardEntity>().SetPathToTargetPosition(aStar(tilePosition));
    }

    void FixedUpdate() {

    }

    private void OnEnemyDestroyed(GameObject goDestroyed) {
      // if (goDestroyed.tag == "EnemyPiece") activeEnemyCount--;
      if (!HasEnemiesRemaining()) GameMaster.Instance.EndNighttime();
    }

    private bool HasEnemiesRemaining() {
      for (int i = 0; i < _spawnPoints.Count; i++) {
        Vector3Int currTile = _spawnPoints[i];
        if (_gameboard[currTile.x, currTile.y].GetComponent<WaveBehaviour>().HasWaves()) return true;
      }
      if (activeEnemyCount > 0) return true;
      return false;
    }

    private bool IsOnGameboard(Vector3Int tileCoords) {
      if (tileCoords.x < stageParams.StageWidth && tileCoords.x >= 0 && tileCoords.y < stageParams.StageLength && tileCoords.y >= 0) {
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

    public List<Vector3> aStar(Vector3Int startTilePos) {
      Piece[,] boardRef = new Piece[stageParams.StageWidth, stageParams.StageLength];
      for (int i = 0; i < stageParams.StageWidth; i++) {
        for (int j = 0; j < stageParams.StageLength; j++) {
          boardRef[i, j] = _gameboard[i, j].GetComponent<GameboardPiece>().piece.CreatePiece();
        }
      }

      _pathfinder.parseGameBoard(boardRef);
      List<PathNode> path = _pathfinder.findPath(startTilePos.x, startTilePos.y, _hearthTileRef.x, _hearthTileRef.y);
      List<Vector3> convertedPath = new List<Vector3>();
      foreach (PathNode node in path) {
        Vector3Int tilePos = new Vector3Int(node.getX(), node.getY(), 0);
        convertedPath.Add(GetWorldPositionFromTilePosition(tilePos));
      }
      return convertedPath;
    }

    public Vector3 GetHearthTile() {
      return GetWorldPositionFromTilePosition(_hearthTileRef);
    }
}

public static class MouseData {
  public static GameObject tempItemBeingDragged;
  public static GameObject tempPieceBeingDragged;
  public static GameObject slotHoveredOver;
  public static Piece tileHoveredOver;
  public static UserInterface interfaceMouseIsOver;
  public static GameObject activeSelection;
  public static GameObject hoverTarget;
  public static Vector3 GetWorldPosition { get { Vector3 normalZmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); normalZmousePos.z = 0; return normalZmousePos; }}
}
