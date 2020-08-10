using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : GameboardEntity {

  GameObject[] AttackableTiles;
  public void SetDronePosition(Vector3 newPos) {
    Vector3Int tilePos = Gameboard.Instance.GetTilePositionFromWorldPosition(newPos);
    // Need Range Check here, 2 for now
    AttackableTiles = Gameboard.Instance.GetTileReferences(tilePos.x, 1, tilePos.y, 1);
  }

  void ScanTilesForEnemies() {
    if (AttackableTiles != null) {
      for(int i = 0; i < AttackableTiles.Length; i++) {
        // AttackableTiles.GetComponent<GameboardPiece>();
      }
    }
  }

  public void ListenToRecall() {
    List<Vector3> path = new List<Vector3>();
    path.Add(Gameboard.Instance.GetHearthTile());
    SetPathToTargetPosition(path);
    StartCoroutine(ReturnToBase());
  }

  public IEnumerator ReturnToBase() {
    while (Vector3.Distance(transform.position, movementPath[0]) > 0.2f) {
      yield return new WaitForSeconds(0.15f);
    }
  }
}
