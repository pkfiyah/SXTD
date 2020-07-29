using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstructionButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject constructablePrefab;

    public void OnBeginDrag(PointerEventData eventData) {
      MouseData.tempPieceBeingDragged = constructablePrefab;
    }

    public void OnDrag(PointerEventData eventData) {
      Vector3 cam = Camera.main.ScreenToWorldPoint(eventData.position);
      cam.z = 0f;
      MouseData.tempPieceBeingDragged.transform.position = Gameboard.Instance.GetWorldPositionFromTilePosition(Gameboard.Instance.GetTilePositionFromWorldPosition(cam));
    }

    public void OnEndDrag(PointerEventData eventData) {
      if (GameMaster.Instance.MakePurchase(1)) {
        GameMaster.Instance.PlaceGameboardPiece(MouseData.tempPieceBeingDragged, Gameboard.Instance.GetTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(eventData.position)));
      }
      MouseData.tempPieceBeingDragged = null;
    }
}
