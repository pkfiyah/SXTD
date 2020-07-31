using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameboardPiece : MonoBehaviour {
    // Delegate used for notifying when a piece is destroyed
    public delegate void PieceDestructionDelegate (GameObject piece);
    public PieceDestructionDelegate pieceDestructionDelegate;

    /** Piece Vars **/
    public PieceObject piece;

    protected ModifiableInt maxHealth;
    private int currentHealth;
    protected ModifiableInt damage;
    public int GetPieceDamage { get { return damage.ModifiedValue; } }
    protected ModifiableFloat attackSpeed;
    public float GetPieceAttackSpeed { get { return attackSpeed.ModifiedValue; } }
    protected ModifiableFloat range;
    public float GetPieceRange { get { return range.ModifiedValue; } }
    private CircleCollider2D rangeCollider;
    /** End Piece Vars **/

    private SpriteRenderer rend;
    public List<GameObject> entitiesInRange = new List<GameObject>();

    public virtual void Awake() {
      // Any piece with a range needs a trigger collider for detecting things in range
      damage = new ModifiableInt(piece.data.baseDamage);

      attackSpeed = new ModifiableFloat(piece.data.baseAttackSpeed);
      range = new ModifiableFloat(piece.data.baseRange);
      if (rangeCollider == null && GetPieceRange > 0f) {
        rangeCollider = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        rangeCollider.isTrigger = true;
        rangeCollider.offset = new Vector2(0f, 0f);
        rangeCollider.radius = GetPieceRange;
      }

      if (piece.data.IsDamagable()) {
         maxHealth = new ModifiableInt(piece.data.baseHealth);
         currentHealth = maxHealth.ModifiedValue;
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

    public void TakeDamage(int damage) {
      currentHealth -= damage;
      if (currentHealth <= 0f) {
        if (pieceDestructionDelegate != null) pieceDestructionDelegate(gameObject);
        Destroy(gameObject);
      }
    }

    public Vector3 GetPosition() {
      return transform.position;
    }

    public Vector3Int GetTilePosition() {
      return Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position);
    }

    // Tagged Triggers will exist on all pieces to represent hitboxes.
    // They are always children of the parent gameboardPieces
    void OnTriggerEnter2D(Collider2D otherCollider) {
      if (GetPieceRange <= 0f) return;
      if (otherCollider.gameObject.tag == "Untagged") return;
      if (otherCollider.gameObject.transform.parent.gameObject == null) return;
      EntityEnteredRange(otherCollider.gameObject.transform.parent.gameObject);
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
      if (GetPieceRange <= 0f) return;
      if (otherCollider.gameObject.tag == "Untagged") return;
      if (otherCollider.gameObject.transform.parent.gameObject == null) return;
      EntityExitedRange(otherCollider.gameObject.transform.parent.gameObject);
    }

    public virtual void EntityEnteredRange(GameObject go) {
      entitiesInRange.Add(go);
    }

    public virtual void EntityExitedRange(GameObject go) {
      entitiesInRange.Remove(go);
    }
}
