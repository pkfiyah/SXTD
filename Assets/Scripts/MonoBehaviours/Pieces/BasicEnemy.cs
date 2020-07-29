using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class BasicEnemy : EntityPiece {

  private bool isAttacking = false;

  // public override void EntityEnteredRange(GameObject go) {
  //   if (go.tag.Equals("PlayerPiece")) {
  //     entitiesInRange.Insert(entitiesInRange.Count == 0 ? 0 : entitiesInRange.Count - 1, go); //ensures hearthtile is always attacked last for priority
  //     StartAttacking();
  //     go.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
  //   } else if (go.tag.Equals("HearthTile")) {
  //     entitiesInRange.Add(go);
  //     StartAttacking();
  //     go.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnPlayerPieceDestroy;
  //   }
  // }
  //
  // public override void EntityExitedRange(GameObject go) {
  //   if (go.tag.Equals("PlayerPiece") || go.tag.Equals("HearthTile")) {
  //     entitiesInRange.Remove(go);
  //     go.GetComponent<GameboardPiece>().pieceDestructionDelegate -= OnPlayerPieceDestroy;
  //   }
  // }

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
      entitiesInRange[0].GetComponent<GameboardPiece>().TakeDamage(10);
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
