using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BasicEnemy : Piece {
  public float _health;
  private Vector2 _targetTilePos;
  private Rigidbody2D _rbody;
  private float movementSpeed = 0.9f;
  private List<Vector2> movementPath;
  private HealthBar _healthBar;

  void Awake() {
    _health = 200.0f;
    _healthBar = this.GetComponentsInChildren(typeof(HealthBar), true)[0] as HealthBar;
    _healthBar.setMaxHealth(_health);
    //_healthBar.setActive(false);
    // GameObject hearthTile = GameObject.FindWithTag("HearthTile");
    // if (hearthTile == null) {
    //   Debug.Log("Missing Hearth");
    // } else {
    //     _target = hearthTile.GetComponent<IBaseEntity>();
    // }
    _rbody = GetComponent<Rigidbody2D>();
    // _targetTile = getClosestTarget(playerPieces);
  }

  void FixedUpdate() {
    // Bad Code move later
    if (_healthBar.getHealth() != _health) {
      _healthBar.setHealth(_health);
    }

    if(GameMaster.Instance.isPlanning) {
      // Do Nothing
      if (_health > 0.0f) {
        _healthBar.setActive(false);
      }
    } else {
      if (_health < 200.0f && !_healthBar.getActive()) {
        _healthBar.setActive(true);
      }

      if (_health <= 0.0f) {
        this.DestroySelf();
      }


      if (movementPath == null) {
        movementPath = GameMaster.Instance.aStar(this.GetTilePosition());
        _targetTilePos = movementPath[0];
        movementPath.RemoveAt(0);
      }
      Vector3Int currentTile = this.GetTilePosition();
      Vector2 currTilePos = new Vector2(currentTile.x, currentTile.y);
      Vector2 currWorldPos = _rbody.position;
      // Find hearth and attack it
      if (_targetTilePos == currTilePos && movementPath.Count > 0) {
          _targetTilePos = movementPath[0];
          movementPath.RemoveAt(0);
      }
      // Vector2 inputVector = new Vector2(_target.x - currTile.x, _target.y - currTile.y);

      Vector3 targetWorldPos = GameMaster.Instance.getWorldPositionFromTilePosition(new Vector3Int((int)_targetTilePos.x, (int)_targetTilePos.y, 0));
      Vector2 inputVector = new Vector2(targetWorldPos.x - currWorldPos.x, targetWorldPos.y - currWorldPos.y);
      inputVector = Vector2.ClampMagnitude(inputVector, 1);
      Vector2 movement = inputVector * movementSpeed;
      Vector2 newPos = currWorldPos + movement * Time.fixedDeltaTime;
      _rbody.MovePosition(newPos);
      // Debug.Log("BasicEnemy: Also Do Nothing");
    }
  }

  void OnMouseDown() {
    Debug.Log("Tickle");
    if (!GameMaster.Instance.isPlanning){
      if (_health > 0.0f) {
        Debug.Log("Ouch");
        this.takePhysDamage(20.0f);
      }
    }
  }

  void OnGUI() {
    Handles.Label(Vector3.zero, "");
    // Handles.DrawLine(this.getWorldPosition(), _target.getWorldPosition());
  }

  public void takePhysDamage(float damageTaken) {
    _health -= damageTaken * ((100.0f - ((10.0f) * 2.25f)) / 100.0f);
    Debug.Log("Phys Hit: " + _health);
    _healthBar.setHealth(_health);
  }

  public void takeMagDamage(float damageTaken) {
    _health -= damageTaken * (1.0f);
    Debug.Log("Mag Hit: " + _health);
    _healthBar.setHealth(_health);
  }
}
