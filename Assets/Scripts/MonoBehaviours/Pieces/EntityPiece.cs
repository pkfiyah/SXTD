using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class EntityPiece : GameboardPiece {
    private GameboardPiece attackTarget;
    private Rigidbody2D rigidbody;
    private float movementSpeed = 0.4f;
    private List<Vector3Int> movementPath;
    protected IsoCharacterRenderer isoRend;
    private bool attacking = false;

    public override void Awake() {
      base.Awake();
      isoRend = GetComponent<IsoCharacterRenderer>();
      rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPathToTargetPosition(List<Vector3Int> newPath) {
      movementPath = newPath;
    }

    // void Update() {
    //   if (entitiesInRange.Count > 0 && !attacking) {
    //     isoRend.SetAttacking();
    //     StartCoroutine(Attacking());
    //   }
    // }

    // IEnumerator Attacking() {
    //   attacking = true;
    //   enemiesInRange[0].GetComponent<GameboardPiece>().TakeDamage(10f);
    //   yield return new WaitForSeconds(1f);
    //   attacking = false;
    // }

    void FixedUpdate() {
      if (movementPath == null) {
        Debug.Log("No Path Found");
        return;
      }

      if (movementPath.Count > 0) {
        Vector2 currWorldPos = rigidbody.position;

        // Find hearth and attack it
        Vector3 targetWorldPos = Gameboard.Instance.GetWorldPositionFromTilePosition(movementPath[0]);
        if (movementPath[0] == GetTilePosition() && movementPath.Count > 1) {
            movementPath.RemoveAt(0);
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
