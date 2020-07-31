using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapPiece : SlottableGameboardPiece {
    private Animator animator;
    private bool attacking = false;
    public HazardDamage DoHazardDamage;

    public override void Awake() {
      base.Awake();
      DoHazardDamage = new HazardDamage();
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
      DoHazardDamage.Invoke(GetPieceDamage);
      yield return new WaitForSeconds(1f);
      attacking = false;
    }

    void OnEnemyDestroy(GameObject enemy) {
      DoHazardDamage.RemoveListener(enemy.GetComponent<GameboardPiece>().TakeDamage);
      entitiesInRange.Remove(enemy);
    }

    public override void EntityEnteredRange(GameObject go) {
      if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
        go.GetComponent<EntityPiece>().pieceDestructionDelegate += OnEnemyDestroy;
        DoHazardDamage.AddListener(go.GetComponent<GameboardPiece>().TakeDamage);
        entitiesInRange.Add(go);
      }
    }

    public override void EntityExitedRange(GameObject go) {
      if (go.GetComponent<EntityPiece>().hitboxTrigger.tag.Equals("EnemyPiece")) {
        go.GetComponent<EntityPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
        DoHazardDamage.RemoveListener(go.GetComponent<GameboardPiece>().TakeDamage);
        entitiesInRange.Remove(go);
      }
    }
}

public class HazardDamage : UnityEvent<int> { }
