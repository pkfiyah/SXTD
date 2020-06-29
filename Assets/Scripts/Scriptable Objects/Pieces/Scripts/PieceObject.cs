using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PieceType {
  Empty,
  Tower,
  Wall,
  Floor,
  Entity,
  SpawnPoint,
  Hearth
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

  public bool IsTraversable() {
    switch(type) {
      case PieceType.Floor:
      case PieceType.Empty:
      case PieceType.Entity:
      case PieceType.Hearth:
        return true;
      default:
        return false;
    }
  }
}
