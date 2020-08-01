using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class GameboardPiece : MonoBehaviour {
    // Delegate used for notifying when a piece is destroyed
    // public delegate void PieceDestructionDelegate (GameObject piece);
    // public PieceDestructionDelegate pieceDestructionDelegate;

    public TileDamage TileDamageEvent = new TileDamage();

    /** Piece Vars **/
    public PieceObject piece;
    // protected ModifiableInt maxHealth;
    // private int currentHealth;

    protected ModifiableInt damage;
    public int GetPieceDamage { get { return damage.ModifiedValue; } }

    protected ModifiableFloat attackSpeed;
    public float GetPieceAttackSpeed { get { return attackSpeed.ModifiedValue; } }

    protected ModifiableInt range;
    public float GetPieceRange { get { return range.ModifiedValue; } }
    /** End Piece Vars **/

    private SpriteRenderer pieceRenderer;
    private Tile pieceTile;
    protected int entitiesInTile;

    public virtual void Awake() {
      // Any piece with a range needs a trigger collider for detecting things in range
      entitiesInTile = 0;
      damage = new ModifiableInt(piece.data.baseDamage);
      attackSpeed = new ModifiableFloat(piece.data.baseAttackSpeed);
      range = new ModifiableInt(piece.data.baseRange);

      // if (piece.data.IsDamagable()) {
      //    maxHealth = new ModifiableInt(piece.data.baseHealth);
      //    currentHealth = maxHealth.ModifiedValue;
      // }
      if (piece.tile != null) pieceTile = piece.tile;

      if (GetComponent<SpriteRenderer>() != null) {
        pieceRenderer = GetComponent<SpriteRenderer>();
        if (piece.data.type == PieceType.GroundConstruction) {
          pieceRenderer.sortingOrder = 0;
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

    // public void TakeDamage(int damage) {
    //   currentHealth -= damage;
    //   if (currentHealth <= 0f) {
    //     if (pieceDestructionDelegate != null) pieceDestructionDelegate(gameObject);
    //     Destroy(gameObject);
    //   }
    // }

    public Vector3 GetPosition() {
      return transform.position;
    }

    public Vector3Int GetTilePosition() {
      return Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position);
    }

    // Tagged Triggers will exist on all pieces to represent hitboxes.
    // They are always children of the parent gameboardPieces
    void OnTriggerEnter2D(Collider2D otherCollider) {
      if (otherCollider.gameObject.tag == "Untagged") return;
      if (otherCollider.tag == gameObject.tag) return;
      EntityEnteredRange(otherCollider.gameObject);
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
      if (otherCollider.gameObject.tag == "Untagged") return;
      if (otherCollider.tag == gameObject.tag) return;
      EntityExitedRange(otherCollider.gameObject);
    }

    public virtual void EntityEnteredRange(GameObject go) {
      // make sure entity of some sort
      entitiesInTile++;
      Debug.Log("Enter: " + GetTilePosition());
      GameboardEntity entityComponent = go.GetComponent<GameboardEntity>();
      //entityComponent.EntityDestructionEvent.AddListener(EntityDestroyedFromTile);
      TileDamageEvent.AddListener(entityComponent.TakeDamage);
    }

    public virtual void EntityExitedRange(GameObject go) {
      Debug.Log("Exit: " + GetTilePosition());
      entitiesInTile--;
      GameboardEntity entityComponent = go.GetComponent<GameboardEntity>();
    //  entityComponent.EntityDestructionEvent.RemoveListener(EntityDestroyedFromTile);
      TileDamageEvent.RemoveListener(entityComponent.TakeDamage);
    }

    // private void EntityDestroyedFromTile(GameObject go) {
    //   //entitiesInTile--;
    //   TileDamageEvent.RemoveListener(go.GetComponent<GameboardEntity>().TakeDamage);
    // }
}

public class TileDamage : UnityEvent<int> { }
