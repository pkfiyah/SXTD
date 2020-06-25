using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerTile : BaseTower {
  public int _maxProj = 3;
  public float _fireRate = 2.0f;
  public GameObject ho;

  private int _currProj = 0;
  private float _fireTimer = 0.0f;
  private List<Projectile> _projectiles;
  private GameObject _focusTarget;

  public override void Awake() {
    base.Awake();
    _projectiles = new List<Projectile>();
  }

  void FixedUpdate() {
    if(GameMaster.Instance.isPlanning) {
      // Do Nothing
    } else {
        _fireTimer += Time.deltaTime;

      // Fires Projectile
      if (_currProj < _maxProj && _fireTimer >= _fireRate) {
        _projectiles.Add(Instantiate(ho, GameMaster.Instance.getWorldPositionFromTilePosition(this.GetTilePosition()), Quaternion.identity).GetComponent<Projectile>());
        _fireTimer = 0.0f;
        if (_focusTarget != null) _projectiles[_projectiles.Count - 1].setTarget(_focusTarget);
      }
    }
  }

  void OnTriggerEnter2D(Collider2D col) {
    // New enemy in range
    if (col.gameObject.layer == LayerMask.NameToLayer("Grunts")) {
      // No current focus
      if (_focusTarget == null) {
        _focusTarget = col.gameObject;
        foreach (Projectile p in _projectiles) {
          if (p != null) {
            //Hit em
            p.setTarget(col.gameObject);
          }
        }
        // _projectiles.Clear();
      }
    }
  }
}
