using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPiece : SlottableGameboardPiece {
    private Animator animator;
    private bool attacking = false;

    public override void Awake() {
      base.Awake();
      ui = GetComponentInChildren<StaticInterface>();
      animator = GetComponent<Animator>();
    }

    void Update() {
      if (entitiesInRange.Count > 0 && !attacking) {
        StartCoroutine(Attacking());
      }
    }

    IEnumerator Attacking() {
      attacking = true;
      animator.SetTrigger("Attacking");
      var loopCount = entitiesInRange.Count;
      if (loopCount < 1) yield return null;
      for (int i = loopCount - 1; i >= 0; i--) {
        if (entitiesInRange[i] != null) entitiesInRange[i].GetComponent<GameboardPiece>().TakeDamage(GetPieceDamage);
      }
      yield return new WaitForSeconds(1f);
      attacking = false;
    }

    void OnEnemyDestroy(GameObject enemy) {
      entitiesInRange.Remove(enemy);
    }

    public override void EntityEnteredRange(GameObject go) {
      Debug.Log("Entered Trap Range");
      Debug.Log("Entitty Tag: " + go.GetComponent<EntityPiece>().hitboxTrigger.tag);
      if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
        entitiesInRange.Add(go);
        go.GetComponent<EntityPiece>().pieceDestructionDelegate += OnEnemyDestroy;
      }
    }

    public override void EntityExitedRange(GameObject go) {
      Debug.Log("Exited Trap Range");
      if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
        entitiesInRange.Remove(go);
        go.GetComponent<EntityPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
      }
    }
}
