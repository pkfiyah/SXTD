using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

  [SerializeField] private Canvas canvas;
  private RectTransform rectTransform;
  private CanvasGroup canvasGroup;

  private void Awake() {
    rectTransform = GetComponent<RectTransform>();
    canvasGroup = GetComponent<CanvasGroup>();
  }

  public void OnBeginDrag(PointerEventData eventData) {
    canvasGroup.blocksRaycasts = false;
    canvasGroup.alpha = 0.6f;
    Debug.Log("BDrag");
  }

  public void OnDrag(PointerEventData eventData) {
    rectTransform.anchoredPosition += eventData.delta;
    Debug.Log("Drag");
  }

  public void OnEndDrag(PointerEventData eventData) {
    canvasGroup.blocksRaycasts = true;
    canvasGroup.alpha = 1f;
    Debug.Log("EDrag");
  }

  public void OnPointerDown(PointerEventData eventData) {
  }

  public void OnDrop(PointerEventData eventData) {

  }
}
