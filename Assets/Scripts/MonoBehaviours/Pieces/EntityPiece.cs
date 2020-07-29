using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class EntityPiece : GameboardPiece {
    public GameObject hitboxTrigger;

    private GameboardPiece attackTarget;
    private Rigidbody2D rigidbody;
    private float movementSpeed = 0.4f;
    private List<Vector3Int> movementPath;
    protected IsoCharacterRenderer isoRend;
    private bool attacking = false;
    private Vector3 tilePositionVariance;

    public override void Awake() {
      base.Awake();
      isoRend = GetComponent<IsoCharacterRenderer>();
      rigidbody = GetComponent<Rigidbody2D>();
      tilePositionVariance = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.2f, 0.2f), 0f);
    }

    public void SetPathToTargetPosition(List<Vector3Int> newPath) {
      movementPath = newPath;
    }

    void FixedUpdate() {
      if (movementPath == null) {
        Debug.Log("No Path Found");
        return;
      }

      if (movementPath.Count > 0) {
        Vector2 currWorldPos = rigidbody.position;

        // Find hearth and attack it
        Vector3 targetWorldPos = Gameboard.Instance.GetWorldPositionFromTilePosition(movementPath[0]) + tilePositionVariance;

        if (movementPath[0] == GetTilePosition() && movementPath.Count > 1) {
            movementPath.RemoveAt(0);
            tilePositionVariance = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.12f, 0.12f), 0f);
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

    public override void EntityEnteredRange(GameObject go) {
      if (go.tag == hitboxTrigger.tag) return;
      // if (go.tag.Equals("EnemyPiece")) {
      //   entitiesInRange.Add(go.transform.parent.gameObject);
      //   go.transform.parent.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate += OnEnemyDestroy;
      // }
    }

    public override void EntityExitedRange(GameObject go) {
      if (go.tag == hitboxTrigger.tag) return;
      // if (go.tag.Equals("EnemyPiece")) {
      //   entitiesInRange.Remove(go.transform.parent.gameObject);
      //   go.transform.parent.gameObject.GetComponent<GameboardPiece>().pieceDestructionDelegate -= OnEnemyDestroy;
      // }
    }
}
