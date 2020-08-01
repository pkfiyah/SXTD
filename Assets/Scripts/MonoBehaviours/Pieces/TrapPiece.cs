using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapPiece : SlottableGameboardPiece {
    private Animator animator;
    private bool attacking = false;

    public override void Awake() {
      base.Awake();
      ui = GetComponentInChildren<StaticInterface>();
      animator = GetComponent<Animator>();
    }

    void Update() {
      Debug.Log("entitiesInTile: " + entitiesInTile);
      if (entitiesInTile > 0 && !attacking) {
        StartCoroutine(Attacking());
      }
    }

    IEnumerator Attacking() {
      attacking = true;
      animator.SetTrigger("Attacking");
      TileDamageEvent.Invoke(GetPieceDamage);
      yield return new WaitForSeconds(1f);
      attacking = false;
    }

    // void OnEnemyDestroy(GameObject enemy) {
    //   TileDamageEvent.RemoveListener(enemy.GetComponent<GameboardPiece>().TakeDamage);
    //   // entitiesInRange.Remove(enemy);
    // }

    // public override void EntityEnteredRange(GameObject go) {
    //   if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
    //     go.GetComponent<EntityPiece>().pieceDestructionDelegate += OnEnemyDestroy;
    //     TileDamageEvent.AddListener(go.GetComponent<GameboardPiece>().TakeDamage);
    //     // entitiesInRange.Add(go);
    //   }
    // }
    //
    // public override void EntityExitedRange(GameObject go) {
    //   if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
    //     go.GetComponent<EntityPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
    //     TileDamageEvent.RemoveListener(go.GetComponent<GameboardPiece>().TakeDamage);
    //     // entitiesInRange.Remove(go);
    //   }
    // }
}
