using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrismiteDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public void OnBeginDrag(PointerEventData eventData) {
      Debug.Log("[PDragH] Initial Pos: " + transform.position);
    }

    public void OnDrag(PointerEventData eventData) {
      Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      pos.z = 0;
      transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData) {
      Vector3Int tilePos = GameMaster.Instance.getTilePositionFromWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      Piece p = GameMaster.Instance.getPieceAtTile(tilePos);
      if (p.isSlottable()) {
        // Do thing
        Debug.Log("[PDragH]Slot here:" + eventData.pointerDrag);
        // p.slotPrismite();
      } else {
        transform.localPosition = Vector3.zero;
      }
    }
}
