using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PieceType {
  Entity = -1,
  Empty = 0,
  Hearth = 1,
  Prismite = 2,
  Construction = 3,
  GroundConstruction = 4,
  UnstableGround = 5,
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
  public float maxHealth;
  public PieceType type;
  public ModifiableInt damage;
  public float attackSpeed;

  public Piece() {
    maxHealth = 100f;
    type = PieceType.Empty;
    attackSpeed = 0f;
    damage = new ModifiableInt();
  }

  public Piece(PieceObject po) {
    maxHealth = po.data.maxHealth;
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
      case PieceType.Empty:
      case PieceType.Prismite:
      case PieceType.UnstableGround:
      case PieceType.GroundConstruction:
      case PieceType.Hearth:
        return true;
      default:
        return false;
    }
  }

  public bool CanConstructOn() {
    switch(type) {
      case PieceType.Empty:
        return true;
      default:
        return false;
    }
  }

  public bool IsDamagable() {
    switch(type) {
      case PieceType.Entity:
      case PieceType.Hearth:
        return true;
      default:
        return false;
    }
  }
}
