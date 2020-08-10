using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class GameboardEntity : MonoBehaviour {

  public EntityObject entity;

  private GameboardPiece attackTarget;
  private Rigidbody2D rigidbody;
  private float movementSpeed = 0.4f;
  protected List<Vector3> movementPath;
  protected IsoCharacterRenderer isoRend;
  private bool attacking = false;
  private int currentHealth;
  private Vector3 tilePositionVariance;
  public bool variantPos = false;
  public int maxHealth;
  public EntityDestruction EntityDestructionEvent = new EntityDestruction();
  public bool IsDamagable;

  public void Awake() {
    movementSpeed = entity.data.baseSpeed;
    IsDamagable = false;
    isoRend = GetComponent<IsoCharacterRenderer>();
    rigidbody = GetComponent<Rigidbody2D>();
    if (variantPos) tilePositionVariance = new Vector3(Random.Range(-0.12f, 0.12f), Random.Range(-0.12f, 0.12f), 0f);
  }

  public void TakeDamage(int damage) {
    currentHealth -= damage;
    if (currentHealth <= 0) {
      EntityDestructionEvent.Invoke(this.gameObject);
      Destroy(gameObject);
    }
  }

  public void SetPathToTargetPosition(Vector3 path) {
    List<Vector3> newPath = new List<Vector3>();
    newPath.Add(path);
    SetPathToTargetPosition(newPath);
  }

  public void SetPathToTargetPosition(List<Vector3> newPath) {
    movementPath = newPath;
  }

  // public void EntityEnteredRange(GameObject go) {
  //   if (go.tag == hitboxTrigger.tag) return;
  // }
  //
  // public void EntityExitedRange(GameObject go) {
  //   if (go.tag == hitboxTrigger.tag) return;
  // }

  void FixedUpdate() {
    if (movementPath == null) {
      return;
    }

    if (movementPath.Count > 0) {
      Vector2 currWorldPos = rigidbody.position;
      // Find hearth and attack it
      Vector3 targetWorldPos = movementPath[0];
      if (variantPos) targetWorldPos += tilePositionVariance;
      if (Vector3.Distance(movementPath[0], transform.position) < 0.1f && movementPath.Count > 1) {
          movementPath.RemoveAt(0);
          if (variantPos) tilePositionVariance = new Vector3(Random.Range(-0.12f, 0.12f), Random.Range(-0.12f, 0.12f), 0f);
      }

      Vector2 inputVector = new Vector2(targetWorldPos.x - currWorldPos.x, targetWorldPos.y - currWorldPos.y);
      inputVector = Vector2.ClampMagnitude(inputVector, 1);
      if (isoRend != null) {
        isoRend.SetDirection(inputVector);
      }
      Vector2 movement = inputVector * movementSpeed;
      Vector2 newPos = currWorldPos + movement * Time.fixedDeltaTime;
      rigidbody.MovePosition(newPos);
    }
  }
}

public class EntityDestruction : UnityEvent<GameObject> { }
