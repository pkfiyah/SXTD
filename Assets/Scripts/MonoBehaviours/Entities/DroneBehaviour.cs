﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : GameboardEntity {

  Vector3 wanderPosition;
  bool wanderUpdate = true;

  void Update() {
    if (wanderUpdate) {
      Debug.Log("MovePAth: " + movementPath);
      wanderUpdate = false;
      StartCoroutine(SelectWanderPoint());
    }
  }

  IEnumerator SelectWanderPoint() {
    yield return new WaitForSeconds(4);
    if (movementPath == null) movementPath = new List<Vector3>();
    movementPath.Add(Gameboard.Instance.GetTileCenterWorldPosition(transform.position) + new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f), 0f));
    wanderUpdate = true;
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
