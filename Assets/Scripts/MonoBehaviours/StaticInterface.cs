using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class StaticInterface : UserInterface {

  public GameObject[] slots;
  public bool isPieceInventory = false;
  private CanvasGroup _cGroup;
  private Canvas canvas;
  private RectTransform uiPosition;

  public override void Awake() {
    if (isPieceInventory) {
      inventory = Instantiate(inventory);
    }
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = Camera.main;
    canvas.sortingLayerName = "UILayer";
    canvas.sortingOrder = 4;
    uiPosition = transform.Find("UIGroup").GetComponent<RectTransform>();
    base.Awake();
    if (isPieceInventory) {
      transform.parent.GetComponent<SlottableGameboardPiece>().ListenToSlots();
    }
    _cGroup = GetComponent<CanvasGroup>();
  }

  public override void createSlots() {
    slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    for (int i = 0; i < inventory.GetSlots.Length; i++) {
      var obj = slots[i];

      addEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
      addEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
      addEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
      addEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
      addEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
      inventory.GetSlots[i].slotDisplay = obj;
      slotsOnInterface.Add(obj, inventory.GetSlots[i]);
    }
  }

  public void Disappear() {
    _cGroup.alpha = 0f;
    _cGroup.blocksRaycasts = false;
  }

  public void Reappear(Vector3 newPosition) {
    // Debug.Log("WhatL " + uiPosition.localScale);
    // Debug.Log("PAretn: " + uiPosition.parent.localScale.x);
    // newPosition.z = 0f;
    _cGroup.alpha = 1f;
    _cGroup.blocksRaycasts = true;
    // Debug.Log("Screen Pos: " + Camera.main.WorldToScreenPoint(newPosition));
    // Vector3 updatedPos = Camera.main.WorldToScreenPoint(newPosition);
    // updatedPos.z = 0f;
    // Debug.Log("Updated Pos: " + updatedPos);
    // Vector2 gimme;
    // RectTransformUtility.ScreenPointToLocalPointInRectangle(uiPosition, updatedPos, Camera.main, out gimme);
    // Debug.Log("Screen Point in Local Point: " + gimme);
    //uiPosition.localPosition = gimme;
    // uiPosition.localPosition = Vector3.zero;
    uiPosition.localPosition = new Vector3(0, 0, 0);// GetOffset(newPosition);
    Debug.Log("Local Pos: " + uiPosition.localPosition);
  }

  private Vector3 GetOffset(Vector3 newPosition) {
    Vector3 updatedPos = Camera.main.WorldToScreenPoint(newPosition);

    Vector3 scaleFactor =  new Vector3(0.5f, 0.25f, 0f);

    updatedPos.x = scaleFactor.x * updatedPos.x;
    updatedPos.y = scaleFactor.y * updatedPos.y;
    updatedPos.z = scaleFactor.z * updatedPos.z;
    return updatedPos;
  }
}
