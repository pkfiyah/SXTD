using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameboardPiece : MonoBehaviour {
    public PieceObject piece;
    public delegate void PieceDestructionDelegate (GameObject piece);
    public PieceDestructionDelegate pieceDestructionDelegate;

    private State currentState = State.Planning;
    private SpriteRenderer rend;
    private float currentHealth;

    public virtual void Awake() {
      if (piece.data.IsDamagable()){
         currentHealth = piece.data.maxHealth;
         Debug.Log("My health: " + piece.data.maxHealth);
      }
      TDEvents.AfterStateChange.AddListener(OnAfterStateChange);
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

    public void OnBeforeStateChange() {
      // Do this
    }

    public void OnAfterStateChange(State state) {
      currentState = state;
    }

    void OnDestroy() {
      if (pieceDestructionDelegate != null) pieceDestructionDelegate(gameObject);
    }

    public void TakeDamage(float damage) {
      currentHealth -= damage;
      Debug.Log("currentHealth: " + currentHealth);
      if (currentHealth <= 0f) {
        Destroy(this.gameObject);
      }
    }

    public Vector3 GetPosition() {
      return transform.position;
    }
}
