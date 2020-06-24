using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Piece : MonoBehaviour, IBaseEntity {
  public Tile tile;
  protected float _health;
  protected float _armour;
  protected float _res;
  protected bool _isTraversable;
  protected bool _isAttackable;
  protected bool _isSlottable;
  public bool isFollowingCursor;

  protected GameMaster _gm;

  public virtual void Awake() {
    _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    _isSlottable = false;
    _isTraversable = true;
    _isAttackable = false;
    isFollowingCursor = false;
  }

  // Update is called once per frame
  void FixedUpdate() {
    void FixedUpdate() {
      if (_gm.isPlanning) {
        // Do Nothing
        if (isFollowingCursor) {
          this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else {
          Debug.Log("Piece: Do Nothing");
        }
      } else {
        Debug.Log("Piece: Also Do Nothing");
      }
    }
  }

  void OnMouseUp() {
    if (_gm.isPlanning && isFollowingCursor) {
      Vector3Int mouseTilePos = _gm.getTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      mouseTilePos.z = 0;
      if (_gm.isOnGameboard(mouseTilePos)) {
        _gm.updateGameboard(mouseTilePos, this.gameObject);
      }
    }
  }

  public void takePhysDamage(float damageTaken) {
    _health -= damageTaken * ((100.0f - ((10.0f +_armour) * 2.25f)) / 100.0f);
  }

  public void takeMagDamage(float damageTaken) {
    _health -= damageTaken * (1.0f - _res);
  }

  public void takePureDamage(float damageTaken) {
    _health -= damageTaken;
  }

  public Vector3Int getTilePosition() {
    return _gm.getTilePositionFromWorldPosition(transform.position);
  }
  public Vector3 getWorldPosition() {
    return transform.position;
  }

  public bool checkAlive() {
    if (_health <= 0.0f) {
      return false;
    } else return true;
  }

  public void destroySelf() {
      Destroy(gameObject);
  }

  public bool isTraversable() {
    return _isTraversable;
  }

  public bool isAttackable() {
    return _isAttackable;
  }

  public bool isSlottable() {
    return _isSlottable;
  }

  public void slotPrismite(Prismite prismite) {
    if (_isSlottable) {
      Debug.Log("Slotted");
    } else {
      Debug.Log("Cannot Slot");
    }
  }
}
