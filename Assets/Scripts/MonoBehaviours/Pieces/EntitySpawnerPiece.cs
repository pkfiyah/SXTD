using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerPiece : SlottableGameboardPiece {

  public GameObject minionObject;
  private GameObject[] minions;

  public override void Awake() {
    // animator = GetComponent<Animator>();
    ui = GetComponentInChildren<StaticInterface>();
    base.Awake();
  }

  private List<Vector3> GetTriangularPositions(Vector3Int tilePosition) {
    Vector3 tileCenterWorld = Gameboard.Instance.GetWorldPositionFromTilePosition(tilePosition);
    List<Vector3> triPositions = new List<Vector3>();
    triPositions.Add(new Vector3(-0.15f, 0.15f, 0f));
    triPositions.Add(new Vector3(0.15f, 0.15f, 0f));
    triPositions.Add(new Vector3(0f, -0.1f, 0f));
    return triPositions;
  }

  public override void OnAfterPlaced() {
    base.OnAfterPlaced();
    minions = new GameObject[3];
    List<Vector3> triPos = GetTriangularPositions(GetTilePosition());
    Vector3Int minPos = GetTilePosition() + new Vector3Int(0, -1, 0);
    for (int i = 0; i < minions.Length; i++) {
      minions[i] = Instantiate(minionObject, GetPosition(), Quaternion.identity);
      minions[i].transform.parent = this.transform.parent;
      minions[i].GetComponent<DroneBehaviour>().SetPathToTargetPosition(Gameboard.Instance.GetWorldPositionFromTilePosition(minPos) + triPos[i]);
    }
  }
}
