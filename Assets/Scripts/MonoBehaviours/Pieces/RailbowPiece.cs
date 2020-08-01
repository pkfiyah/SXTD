using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailbowPiece : SlottableGameboardPiece {
  private bool attacking = false;

  public GameObject lineProjectile;
  private GameObject line;

  public override void Awake() {
    base.Awake();
    ui = GetComponentInChildren<StaticInterface>();
    // animator = GetComponent<Animator>();
  }

  void Update() {
    // if (entitiesInRange.Count > 0 && !attacking) {
    //   StartCoroutine(Attacking());
    // }
  }

  //IEnumerator Attacking() {
    // if (!attacking) {
    //   attacking = true;
    //   GameboardPiece gp = entitiesInRange[0].GetComponent<GameboardPiece>();
    //   Projectile.Create(transform.position, gp, GetPieceDamage);
    //   yield return new WaitForSeconds(2f);
    // }
    // attacking = false;
  //}

  // void OnEnemyDestroy(GameObject enemy) {
  //   entitiesInRange.Remove(enemy);
  // }

  // public override void EntityEnteredRange(GameObject go) {
  //   if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
  //     entitiesInRange.Add(go);
  //     go.GetComponent<EntityPiece>().pieceDestructionDelegate += OnEnemyDestroy;
  //   }
  // }
  //
  // public override void EntityExitedRange(GameObject go) {
  //   if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
  //     entitiesInRange.Remove(go);
  //     go.GetComponent<EntityPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
  //   }
  // }
}
