using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using DataStructures.PriorityQueue;

public class GameMaster : MonoBehaviour {
  // TEST CODE------
  public InventoryObject inventory;
  public Attribute[] attributes;
  // TEST CODE END -- **DELETE ALL REFS ** --




  // Public Statis Reference to the GameMaster
  public static GameMaster Instance { get; private set; }

  public Vector2Int boardDimensions;
  public GameObject emptyTile;
  public GameObject hearth;
  public PieceMenu pm;
  public int spawnNum;
  public bool isPlanning = true;

  // [SerializeField] private UI_Inventory uiInventory;
  private Tilemap _entityTilemap;
  private Pathfinding _pathfinder;
  private Piece[,] _gameboard;
  // private PrismiteCasino _casino;

  // ------------------- THIS IS FOR TOWERS LATER -------------------- //
  // private void Start() {
  //   for (int i = 0; i < attributes.Length; i++) {
  //     attributes[i].SetParent(this);
  //   }
  //   for (int i = 0; i < equipment.GetSlots.Length; i++) {
  //     equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
  //     equipment.GetSlots[i].OnAfterUpdate += OnBeforeSlotUpdate;
  //   }
  // }
  //
  // public void OnBeforeSlotUpdate (InventorySlot _slot) {
  //   Debug.Log("_slot.parent.inventory.type: " + _slot.parent.inventory.type);
  //   if(_slot.ItemObject == null) return; // Only fire when item exists in slot
  //   switch(_slot.parent.inventory.type) {
  //     case InterfaceType.Inventory:
  //       break;
  //     case InterfaceType.Piece:
  //       print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type));
  //       break;
  //     default:
  //       break;
  //   }
  // }
  //
  // public void OnAfterSlotUpdate (InventorySlot _slot) {
  //   Debug.Log("_slot.parent.inventory.type: " + _slot.parent.inventory.type);
  //   switch(_slot.parent.inventory.type) {
  //     case InterfaceType.Inventory:
  //       break;
  //     case InterfaceType.Piece:
  //       print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type));
  //       break;
  //     default:
  //       break;
  //   }
  // }
  // ------------------- Code Fires on Inventory slot change ----------- //

  // Initialize Game State
  void Awake() {
    Instance = this;
    _entityTilemap = GameObject.Find("Entity Tilemap").GetComponent<Tilemap>();
    _pathfinder = new Pathfinding(boardDimensions.x, boardDimensions.y);
    // uiInventory.setInventory(_inventory);

    // Construct Empty Game Board
    _gameboard = new Piece[boardDimensions.x, boardDimensions.y];
    for (int i = 0; i < boardDimensions.x; i++) {
      for (int j = 0; j < boardDimensions.y; j++) {
        Vector3 worldPosition = this.getWorldPositionFromTilePosition(new Vector3Int(i, j, 0));
        this.updateGameboard(new Vector3Int(i, j, 0), null);
      }
    }
  }

  // TEMP STUFF
  public void triggeredThing(PrismiteObject prismite){
    inventory.AddPrismite(new Prismite(prismite));
  }

  // Resets inventory scriptable object
  private void OnApplicationQuit() {
    inventory.Clean();
  }

  // Update is called once per frame
  void FixedUpdate() {
    // Planning phase
    if(isPlanning) {
      if(MouseData.activeSelection != null) {

      }
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      Debug.Log("Saving");
      inventory.Save();
    }
    if (Input.GetKeyDown(KeyCode.Delete)) {
      Debug.Log(":Loading");
      inventory.Load();
    }
  }

  public Piece getPieceAtTile(Vector3Int tilePosition) {
    return _gameboard[tilePosition.x, tilePosition.y];
  }

  public Vector3Int getTilePositionFromWorldPosition(Vector3 worldPosition) {
    Vector3Int ret = _entityTilemap.WorldToCell(worldPosition);
    ret.z = 0;
    return ret;
  }

  public Vector3 getWorldPositionFromTilePosition(Vector3Int tilePosition) {
    Vector3 ret = _entityTilemap.GetCellCenterWorld(tilePosition);
    ret.z = 0f;
    return ret;
  }

  public void setTileGraphic(Vector3Int tilePosition, Tile tile) {
    _entityTilemap.SetTile(tilePosition, tile);
  }

  public void setHearthTile(Vector3Int piecePosition, GameObject piece) {
    if (hearth == null) {
      if (piece.GetComponent<HearthTile>() != null) {
        hearth = updateGameboard(piecePosition, piece);
      }
    } else {
      // Delete Current Hearth, Only One May Exist
      Vector3Int oldTile = hearth.GetComponent<Piece>().getTilePosition();
      updateGameboard(oldTile, null);
      hearth = updateGameboard(piecePosition, piece);
    }
  }

  // All modifications to the game board happen here
  public GameObject updateGameboard(Vector3Int tilePosition, GameObject piece) {
    // If called with null, just make this position an emptyTile
    if (piece == null) piece = Instantiate(emptyTile, getWorldPositionFromTilePosition(tilePosition), Quaternion.identity);
    Piece pieceRef = _gameboard[tilePosition.x, tilePosition.y];
    Piece p = piece.GetComponent<Piece>();
    if (piece.GetComponent<SpriteRenderer>() == null && p.tile != null) {
      p.tile.color = Color.white;
      this.setTileGraphic(tilePosition, p.tile);
    }
    if (pieceRef != null) {
      _gameboard[tilePosition.x, tilePosition.y].destroySelf();
    }
    _gameboard[tilePosition.x, tilePosition.y] = p;
    return piece;
  }

  public bool isOnGameboard(Vector3Int tileCoords) {
    if (tileCoords.x < boardDimensions.x && tileCoords.x >= 0 && tileCoords.y < boardDimensions.y && tileCoords.y >= 0) {
      return true; // On the gameboard
    } else {
      return false;
    }
  }

  public List<Vector2> aStar(Vector3Int startTilePos) {
    _pathfinder.parseGameBoard(_gameboard);
    List<PathNode> path = _pathfinder.findPath(startTilePos.x, startTilePos.y, hearth.GetComponent<Piece>().getTilePosition().x, hearth.GetComponent<Piece>().getTilePosition().y);
    List<Vector2> convertedPath = new List<Vector2>();

    foreach (PathNode node in path) {
      convertedPath.Add(new Vector2(node.getX(), node.getY()));
    }
    return convertedPath;
  }

  // public void AttributeModified(Attribute attribute) {
  //   Debug.Log(string.Concat(attribute.type, "att updated"));
  // }
}

[System.Serializable]
public class Attribute {
  [System.NonSerialized]
  public GameMaster parent;
  // public Attributes type;
  public ModifiableInt value;

  public void SetParent(GameMaster _parent) {
    parent = _parent;
    value = new ModifiableInt(AttributeModified);
  }

  public void AttributeModified() {
    //parent.AttributeModified(this);
  }
}


public static class MouseData {
  public static GameObject tempItemBeingDragged;
  public static GameObject slotHoveredOver;
  public static Piece tileHoveredOver;
  public static UserInterface interfaceMouseIsOver;
  public static GameObject activeSelection;
  public static Vector3 GetWorldPosition { get { Vector3 normalZmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); normalZmousePos.z = 0; return normalZmousePos; }}
  public static Vector3Int GetTilePosition { get { Vector3 normalZmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); normalZmousePos.z = 0; return GameMaster.Instance.getTilePositionFromWorldPosition(normalZmousePos); }}
}
