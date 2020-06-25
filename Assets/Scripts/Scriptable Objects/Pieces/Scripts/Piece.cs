using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
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

public class Piece : MonoBehaviour, IBaseEntity {
  public Tile tile;
  public PieceType type;

  public virtual void Awake() {
  }

  void OnMouseEnter() {
    MouseData.tileHoveredOver = this;
  }

  void OnMouseExit() {
    MouseData.tileHoveredOver = null;
  }

  public Vector3Int GetTilePosition() {
    return GameMaster.Instance.getTilePositionFromWorldPosition(transform.position);
  }
  public Vector3 GetWorldPosition() {
    return transform.position;
  }

  public void DestroySelf() {
      Destroy(gameObject);
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
