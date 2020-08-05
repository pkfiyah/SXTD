using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : GameboardEntity {
  // void OnEnable() {
  //   TDEvents.TimeChange.AddListener(TimeChange);
  // }
  //
  // void OnDisable() {
  //   TDEvents.TimeChange.RemoveListener(TimeChange);
  // }
  //
  // void OnDestroy() {
  //   TDEvents.TimeChange.RemoveListener(TimeChange);
  // }
  //
  // private void TimeChange(int newTime){
  //   List<Vector3> path = new List<Vector3>();
  //   path.Add(Gameboard.Instance.GetHearthTile());
  //   SetPathToTargetPosition(path);
  //   StartCoroutine(ReturnToBase());
  // }
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
