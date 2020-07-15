using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailbowPiece : SlottableGameboardPiece {
  private List<GameObject> enemiesInRange;
  private bool attacking = false;

  public GameObject lineProjectile;
  private GameObject line;

  public override void Awake() {
    base.Awake();
    enemiesInRange = new List<GameObject>();
  }

  void Update() {
    if (enemiesInRange.Count > 0 && !attacking) {
      StartCoroutine(Attacking());
    }
  }

  IEnumerator Attacking() {
    if (!attacking) {
      attacking = true;
      GameboardPiece gp = enemiesInRange[0].GetComponent<GameboardPiece>();
      Projectile.Create(transform.position, gp, piece.data.damage.BaseValue);
      yield return new WaitForSeconds(2f);
    }
    attacking = false;
    // Debug.Log("Distance: " + Vector3.Distance(line.transform.position, enemiesInRange[0].transform.position));
    // while (Vector3.Distance(line.transform.position, enemiesInRange[0].transform.position) > 0.01f) {
    //   line.transform.position = Vector3.Lerp(line.transform.position, enemiesInRange[0].transform.position, 1f * Time.deltaTime);
    //   yield return null;
    // }
    // Debug.Log("Shot! Blam!");
    // enemiesInRange[0].GetComponent<GameboardPiece>().TakeDamage(10f); // Beam in morning
    // attacking = false;
    // Destroy(line);
    // yield return new WaitForSeconds(1f);

  }

  void OnEnemyDestroy(GameObject enemy) {
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
