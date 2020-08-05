using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : GameboardEntity {
  void OnEnable() {
    TDEvents.TimeChange.AddListener(TimeChange);
  }

  void OnDisable() {
    TDEvents.TimeChange.RemoveListener(TimeChange);
  }

  void OnDestroy() {
    TDEvents.TimeChange.RemoveListener(TimeChange);
  }

  private void TimeChange(int newTime){
    List<Vector3Int> path = new List<Vector3Int>();
    path.Add(Gameboard.Instance.GetHearthTile());
    SetPathToTargetPosition(path);
    StartCoroutine(ReturnToBase());
  }

  private IEnumerator ReturnToBase() {
    while (Vector3.Distance(transform.position, movementPath[0]) > 0.1f) {
      Debug.Log("Deistance: " + Vector3.Distance(transform.position, movementPath[0]));
      yield return new WaitForSeconds(0.15f);
    }
    Destroy(gameObject);
  }
}
