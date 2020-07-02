using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BasicEnemy : GameboardPiece {
  public float _health;
  private Rigidbody2D _rbody;
  private float movementSpeed = 0.9f;
  private List<Vector3Int> movementPath;
  // private HealthBar _healthBar;

  void Awake() {
    _health = 200.0f;
    // _healthBar = this.GetComponentsInChildren(typeof(HealthBar), true)[0] as HealthBar;
    // _healthBar.setMaxHealth(_health);
    //_healthBar.setActive(false);
    _rbody = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    // Bad Code move later
    // if (_healthBar.getHealth() != _health) {
    //   _healthBar.setHealth(_health);
    // }

    if(GameMaster.Instance.runState.GetState == State.Planning) {
      // Do Nothing
      if (_health > 0.0f) {
        // _healthBar.setActive(false);
      }
    } else if (GameMaster.Instance.runState.GetState == State.Active) {
      // if (_health < 200.0f && !_healthBar.getActive()) {
      //   _healthBar.setActive(true);
      // }

      if (_health <= 0.0f) {
        Destroy(gameObject);
      }

      if (movementPath == null) {
        movementPath = Gameboard.Instance.aStar(piece.parent.GetTilePositionFromWorldPosition(transform.position));
      }

      if (movementPath.Count > 0) {
        Vector3Int currentTilePos = piece.parent.GetTilePositionFromWorldPosition(transform.position);
        Vector2 currWorldPos = _rbody.position;

        // Find hearth and attack it
        Vector3 targetWorldPos = piece.parent.GetWorldPositionFromTilePosition(movementPath[0]);
        if (movementPath[0] == currentTilePos && movementPath.Count > 0) {
            movementPath.RemoveAt(0);
        }

        Vector2 inputVector = new Vector2(targetWorldPos.x - currWorldPos.x, targetWorldPos.y - currWorldPos.y);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currWorldPos + movement * Time.fixedDeltaTime;
        _rbody.MovePosition(newPos);
      }
    }
  }

  void OnMouseDown() {
    Debug.Log("Tickle");
    if (GameMaster.Instance.runState.GetState != State.Planning){
      if (_health > 0.0f) {
        Debug.Log("Ouch");
        this.takePhysDamage(20.0f);
      }
    }
  }

  public void takePhysDamage(float damageTaken) {
    _health -= damageTaken * ((100.0f - ((10.0f) * 2.25f)) / 100.0f);
    Debug.Log("Phys Hit: " + _health);
    // _healthBar.setHealth(_health);
  }

  public void takeMagDamage(float damageTaken) {
    _health -= damageTaken * (1.0f);
    Debug.Log("Mag Hit: " + _health);
    // _healthBar.setHealth(_health);
  }
}
