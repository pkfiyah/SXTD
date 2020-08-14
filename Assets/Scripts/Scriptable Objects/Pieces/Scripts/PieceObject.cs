using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PieceType {
  Empty = 0,
  Hearth = 1,
  Prismite = 2,
  Construction = 3,
  GroundConstruction = 4,
  UnstableGround = 5,
}

[CreateAssetMenu(fileName = "New Piece Object", menuName = "Inventory System/Piece/New Piece")]
public class PieceObject : ScriptableObject {
  public Sprite staticSprite;
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
  public int baseHealth;
  public int baseDamage;
  public float baseAttackSpeed;
  public int baseRange;
  public int cost;

  public Piece() {
    type = PieceType.Empty;
    baseHealth = 100;
    baseDamage = 0;
    baseAttackSpeed = 0f;
    baseRange = 0;
    cost = 0;
  }

  public Piece(PieceObject po) {
    type = po.data.type;
    baseHealth = po.data.baseHealth;
    baseDamage = po.data.baseDamage;
    baseAttackSpeed = po.data.baseAttackSpeed;
    baseRange = po.data.baseRange;
    cost = po.data.cost;
  }

  public Piece(PieceType pt) {
    type = pt;
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
      case PieceType.Hearth:
        return true;
      default:
        return false;
    }
  }
}
