using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IBaseProjectile {

  public float damage;
  public float speed;
  protected GameObject _target;
  protected Rigidbody2D _rbody;

  public virtual void Awake() {
    _rbody = this.GetComponent<Rigidbody2D>();
  }

  public virtual void FixedUpdate() {
    if (_target != null) {
      // Approach target at speed
      Vector2 currWorldPos = _rbody.position;
      Vector3 targetWorldPos = _target.gameObject.transform.position;
      Vector2 inputVector = new Vector2(targetWorldPos.x - currWorldPos.x, targetWorldPos.y - currWorldPos.y);
      inputVector = Vector2.ClampMagnitude(inputVector, 1);
      Vector2 movement = inputVector * speed;
      Vector2 newPos = currWorldPos + movement * Time.fixedDeltaTime;
      _rbody.MovePosition(newPos);
      // Debug.Log("BasicEnemy: Also Do Nothing");
    }
  }

  public void setTarget(GameObject target) {
    _target = target;
  }

  public void getStats() {

  }

  public void setStats() {

  }

  public void updateProjectileMovement() {

  }

  void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject == _target) {
      // Do thing
      Debug.Log("HIt Thing");
      Piece p = col.gameObject.GetComponent<Piece>();

      p.takePhysDamage(damage);
      Destroy(this.gameObject);
    }
  }
}
