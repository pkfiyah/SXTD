using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;
    public delegate void PieceDestructionDelegate (GameObject piece);
    public PieceDestructionDelegate pieceDestructionDelegate;

    protected List<GameObject> entitiesInRange;
    private SpriteRenderer rend;
    private float currentHealth;
    private CircleCollider2D rangeCollider;

    public virtual void Awake() {
      // Any piece with a range needs a trigger collider for detecting things in range
      if (piece.data.range > 0) {
        entitiesInRange = new List<GameObject>();
        rangeCollider = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        rangeCollider.isTrigger = true;
        rangeCollider.radius = piece.data.range;
      }

      if (piece.data.IsDamagable()){
         currentHealth = piece.data.maxHealth;
      }

      if (GetComponent<SpriteRenderer>() != null) {
        rend = GetComponent<SpriteRenderer>();
        if (piece.data.type == PieceType.GroundConstruction) {
          rend.sortingOrder = 1;
        }
      }
    }

    void OnMouseDown() {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      if (MouseData.activeSelection == null  || (MouseData.activeSelection != null && MouseData.activeSelection != this)) {
        MouseData.activeSelection = this.gameObject;
      } else {
        MouseData.activeSelection = null;
      }
    }

    void OnDestroy() {
      if (pieceDestructionDelegate != null) pieceDestructionDelegate(gameObject);
    }

    public void TakeDamage(float damage) {
      Debug.Log("Taking Damage");
      currentHealth -= damage;
      if (currentHealth <= 0f) {
        Destroy(this.gameObject);
      }
    }

    public Vector3 GetPosition() {
      return transform.position;
    }

    public Vector3Int GetTilePosition() {
      return Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
      if (piece.data.range == 0) return;
      EntityEnteredRange(otherCollider);
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
      if (piece.data.range == 0) return;
      EntityExitedRange(otherCollider);
    }

    public virtual void EntityEnteredRange(Collider2D otherCollider) {
      entitiesInRange.Add(otherCollider.gameObject);
    }

    public virtual void EntityExitedRange(Collider2D otherCollider) {
      entitiesInRange.Remove(otherCollider.gameObject);
    }
}
