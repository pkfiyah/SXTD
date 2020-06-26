using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject tilePrefab;

    public void OnBeginDrag(PointerEventData eventData) {
      MouseData.tempPieceBeingDragged = Instantiate(tilePrefab, MouseData.GetWorldPosition, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData) {
      MouseData.tempPieceBeingDragged.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
      Gameboard.Instance.UpdateGameboard(Gameboard.Instance.GetTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(eventData.position)), MouseData.tempPieceBeingDragged);
      Destroy(MouseData.tempPieceBeingDragged);
    }

}
