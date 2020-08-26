using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerPiece : SlottableGameboardPiece {

  public GameObject minionObject;
  private GameObject[] minions;
  private bool repositioning = false;

  public override void Awake() {
    // animator = GetComponent<Animator>();
    ui = GetComponentInChildren<StaticInterface>();
    base.Awake();
  }

  void Update() {
    if (repositioning) {
      if (Input.GetMouseButtonDown(0)) {
        Vector3Int newTilePosition = Gameboard.Instance.GetTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        repositioning = false;
        TDEvents.RequestReposition.Invoke(null);
        if (IsRepositionInRange(newTilePosition)) {
          for(int i = 0; i < minions.Length; i++) {
            minions[i].GetComponent<GameboardEntity>().SetPathToTargetPosition(Gameboard.Instance.GetWorldPositionFromTilePosition(newTilePosition));
          }
        }
      }
    }
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

  public void RepositionMinionRequest() {
    ui.Disappear();
    TDEvents.RequestReposition.Invoke(this.gameObject);
    repositioning = true;
  }

  private bool IsRepositionInRange(Vector3Int newPosition) {
    int range = piece.data.baseRange;
    Vector3Int tilePosition = GetTilePosition();
    Bounds b = new Bounds(tilePosition, new Vector3(1 + (range * 2), 1 + (range * 2) , 0));
    return b.Contains(newPosition);
  }
}
