using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class BasicEnemy : EntityPiece {
  public override void EntityEnteredRange(Collider2D otherCollider) {
    if (otherCollider.gameObject.tag.Equals("PlayerPiece")) {
      entitiesInRange.Insert(entitiesInRange.Count == 0 ? 0 : entitiesInRange.Count - 1, otherCollider.gameObject); //ensures hearthtile is always attacked last for priority
      otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
    } else if (otherCollider.gameObject.tag.Equals("HearthTile")) {
       if (isoRend != null) isoRend.SetAttacking();
       entitiesInRange.Add(otherCollider.gameObject);
       otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
    }
  }

  public override void EntityExitedRange(Collider2D otherCollider) {
    if (otherCollider.gameObject.tag.Equals("PlayerPiece") || otherCollider.gameObject.tag.Equals("HearthTile")) {
      entitiesInRange.Remove(otherCollider.gameObject);
      otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate -= OnPlayerPieceDestroy;
    }
  }

  private void OnPlayerPieceDestroy(GameObject playerPiece) {
    entitiesInRange.Remove(playerPiece);
  }
}
