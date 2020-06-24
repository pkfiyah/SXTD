using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrismiteDropHandler : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
      RectTransform invPanel = transform as RectTransform;
      Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      pos.z = 0;
      if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition, Camera.main)) {
        Debug.Log("[PDropH]Drop Outside of Panel");
      } else {
        // Was it dropped into a different slot?
        Debug.Log("[PDropH]Drop Inside of Panel");
      }
    }
}
