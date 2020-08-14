using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class GameboardPiece : MonoBehaviour {

    public TileDamage TileDamageEvent = new TileDamage();
    public bool DebugMode = false;

    /** Piece Vars **/
    public PieceObject piece;
    protected ModifiableInt damage;
    public int GetPieceDamage { get { return damage.ModifiedValue; } }

    protected ModifiableFloat attackSpeed;
    public float GetPieceAttackSpeed { get { return attackSpeed.ModifiedValue; } }

    protected ModifiableInt range;
    public float GetPieceRange { get { return range.ModifiedValue; } }
    /** End Piece Vars **/

    protected SpriteRenderer pieceRenderer;
    private Tile pieceTile;
    protected Vector3Int TilePosition;
    protected int entitiesInTile;

    public virtual void Awake() {
      // Any piece with a range needs a trigger collider for detecting things in range
      entitiesInTile = 0;

      damage = new ModifiableInt(piece.data.baseDamage);
      attackSpeed = new ModifiableFloat(piece.data.baseAttackSpeed);
      range = new ModifiableInt(piece.data.baseRange);

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
        TDEvents.SelectionChanged.Invoke(this.gameObject);
      } else {
        MouseData.activeSelection = null;
      }

      if (piece.data.CanConstructOn() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<HeroConstructionCard>() != null) {
        GameMaster.Instance.PlaceGameboardPiece(EventSystem.current.currentSelectedGameObject.GetComponent<HeroConstructionCard>().constructablePiece, GetTilePosition());
      }
    }

    void Update() {
      if (DebugMode) {
        Debug.Log("Check Spawn(U): " + GetTilePosition());
        Debug.Log("Minion Spawn(U): " + GetPosition());
      }
    }

    void OnEnable() {}
    void OnDisable() {}

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
      entitiesInTile++;
      GameboardEntity entityComponent = go.GetComponent<GameboardEntity>();
      TileDamageEvent.AddListener(entityComponent.TakeDamage);
    }

    public virtual void EntityExitedRange(GameObject go) {
      entitiesInTile--;
      GameboardEntity entityComponent = go.GetComponent<GameboardEntity>();
      TileDamageEvent.RemoveListener(entityComponent.TakeDamage);
    }

    void OnMouseOver() {
      MouseData.hoverTarget = this.gameObject;
    }

    // Things that Need to happen after initialization and gameboard placement happen here
    public abstract void OnAfterPlaced();

    void OnDrawGizmos() {

      // Gizmos.color = Color.yellow;
      // Gizmos.DrawWireSphere(transform.position, 0.25f);

    }
}

public class TileDamage : UnityEvent<int> { }
