using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class BasicEnemy : EntityPiece {

  private bool isAttacking = false;

  public override void EntityEnteredRange(Collider2D otherCollider) {
    if (otherCollider.gameObject.tag.Equals("PlayerPiece")) {
      entitiesInRange.Insert(entitiesInRange.Count == 0 ? 0 : entitiesInRange.Count - 1, otherCollider.gameObject); //ensures hearthtile is always attacked last for priority
      StartAttacking();
      otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
    } else if (otherCollider.gameObject.tag.Equals("HearthTile")) {
      entitiesInRange.Add(otherCollider.gameObject);
      StartAttacking();
      otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
    }
  }

  public override void EntityExitedRange(Collider2D otherCollider) {
    if (otherCollider.gameObject.tag.Equals("PlayerPiece") || otherCollider.gameObject.tag.Equals("HearthTile")) {
      entitiesInRange.Remove(otherCollider.gameObject);
      otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate -= OnPlayerPieceDestroy;
    }
  }

  private void StartAttacking() {
    if (!isAttacking) {
      Debug.Log("Start Attacking");
      StartCoroutine(Attacking());
    }
  }

  IEnumerator Attacking() {
    isAttacking = true;
    Debug.Log("InRange: " + entitiesInRange.Count);
    while (entitiesInRange.Count > 0) {
      entitiesInRange[0].GetComponent<GameboardPiece>().TakeDamage(10f);
      Debug.Log("Boom");
      if (isoRend != null) isoRend.SetAttacking();
      yield return new WaitForSeconds(1f);
    }
    isAttacking = false;
  }

  private void OnPlayerPieceDestroy(GameObject playerPiece) {
    entitiesInRange.Remove(playerPiece);
  }
}
