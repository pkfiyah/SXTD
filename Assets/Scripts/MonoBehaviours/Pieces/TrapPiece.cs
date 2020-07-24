﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPiece : SlottableGameboardPiece {
    private List<GameObject> enemiesInRange;
    private Animator animator;
    private bool attacking = false;

    public override void Awake() {
      base.Awake();
      animator = GetComponent<Animator>();
      enemiesInRange = new List<GameObject>();
    }

    void Update() {
      if (enemiesInRange.Count > 0 && !attacking) {
        StartCoroutine(Attacking());
      }
    }

    IEnumerator Attacking() {
      attacking = true;
      animator.SetTrigger("Attacking");
      var loopCount = enemiesInRange.Count;
      Debug.Log("LoopCount: " + loopCount);
      if (loopCount < 1) yield return null;
      for (int i = loopCount - 1; i >= 0; i--) {
        Debug.Log("Enemy: " + enemiesInRange[i]);
        if (enemiesInRange[i] != null) enemiesInRange[i].GetComponent<GameboardPiece>().TakeDamage(10f);
      }
      yield return new WaitForSeconds(1f);
      attacking = false;
      Debug.Log("Attacked");
    }

    void OnEnemyDestroy(GameObject enemy) {
      Debug.Log("Removing: Enemy");
      enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
      if (otherCollider.gameObject.tag.Equals("EnemyPiece")) {
        enemiesInRange.Add(otherCollider.gameObject);
        otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnEnemyDestroy;
      }
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
      if (otherCollider.gameObject.tag.Equals("EnemyPiece")) {
        enemiesInRange.Remove(otherCollider.gameObject);
        otherCollider.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
      }
    }
}
