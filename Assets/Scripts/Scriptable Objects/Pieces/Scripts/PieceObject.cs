using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PieceType {
  Empty = 0,
  Entity = 1,
  Hearth = 2,
  Ground = 3,
  Tower = 4,
  Wall = 5,
  UnstableGround = 6,
  SpawnPoint = 7,
}

[CreateAssetMenu(fileName = "New Piece Object", menuName = "Inventory System/Piece/New Piece")]
public class PieceObject : ScriptableObject {
  public Tile tile; // Sprite shown on grid
  [TextArea(15,20)]
  public string description;
  public Piece data = new Piece();
  [System.NonSerialized]
  public Gameboard parent;

  public Piece CreatePiece() {
    return new Piece(this);
  }
}

[System.Serializable]
public class Piece : IPiece {
  public PieceType type;
  public ModifiableInt damage;
  public float attackSpeed;

  public Piece() {
    type = PieceType.Empty;
    attackSpeed = 0f;
    damage = new ModifiableInt();
  }

  public Piece(PieceObject po) {
    type = po.data.type;
    attackSpeed = po.data.attackSpeed;
    damage = po.data.damage;
  }

  public Piece(PieceType pt) {
    type = pt;
    attackSpeed = 0f;
    damage = new ModifiableInt();
  }

  public bool IsTraversable() {
    switch(type) {
      case PieceType.Ground:
      case PieceType.UnstableGround:
      case PieceType.Empty:
      case PieceType.Entity:
      case PieceType.Hearth:
        return true;
      default:
        return false;
    }
  }

  public bool CanConstructOn() {
    switch(type) {
      case PieceType.Ground:
      case PieceType.Empty:
        return true;
      default:
        return false;
    }
  }
}
