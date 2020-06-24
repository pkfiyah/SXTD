using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrismiteSlot : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
      Debug.Log("[PrismiteSlot] OnDrop: " + eventData.pointerDrag);
      Debug.Log("Dropping On: " + GetComponent<RectTransform>().anchoredPosition);
      if (eventData.pointerDrag != null) {
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
      }
    }
}
