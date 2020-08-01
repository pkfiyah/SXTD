using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class GameboardEntity : MonoBehaviour {

  public EntityObject entity;

  private GameboardPiece attackTarget;
  private Rigidbody2D rigidbody;
  private float movementSpeed = 0.4f;
  private List<Vector3Int> movementPath;
  protected IsoCharacterRenderer isoRend;
  private bool attacking = false;
  private int currentHealth;
  private Vector3 tilePositionVariance;
  public int maxHealth;
  public EntityDestruction EntityDestructionEvent = new EntityDestruction();

  public void Awake() {
    isoRend = GetComponent<IsoCharacterRenderer>();
    rigidbody = GetComponent<Rigidbody2D>();
    tilePositionVariance = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.2f, 0.2f), 0f);
  }

  public void TakeDamage(int damage) {
    currentHealth -= damage;
    if (currentHealth <= 0) {
      Debug.Log("INVOKING DEATH");
      EntityDestructionEvent.Invoke(this.gameObject);
      Destroy(gameObject);
    }
  }

  public void SetPathToTargetPosition(List<Vector3Int> newPath) {
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
      Debug.Log("No Path Found");
      return;
    }

    if (movementPath.Count > 0) {
      Vector2 currWorldPos = rigidbody.position;

      // Find hearth and attack it
      Vector3 targetWorldPos = Gameboard.Instance.GetWorldPositionFromTilePosition(movementPath[0]) + tilePositionVariance;
      if (movementPath[0] == Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position) && movementPath.Count > 1) {
          movementPath.RemoveAt(0);
          tilePositionVariance = new Vector3(Random.Range(-0.12f, 0.12f), Random.Range(-0.12f, 0.12f), 0f);
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
