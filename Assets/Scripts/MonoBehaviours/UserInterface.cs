using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour {

    public InventoryObject inventory;
    public bool canAcceptItems = true;
    public bool destructableInventory = false;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public virtual void Awake() {
        if (inventory == null) {
          inventory = GetComponent<SlottableGameboardPiece>().inventory;
        }
        if (destructableInventory) {
          inventory = Instantiate(inventory);
        }

        for (int i = 0; i < inventory.GetSlots.Length; i++) {
          inventory.GetSlots[i].parent = this;
          inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        createSlots();
        addEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject);});
        addEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject);});
    }

    private void OnSlotUpdate(InventorySlot _slot) {
      if (_slot.prismite.id >= 0) {
        _slot.slotDisplay.transform.Find("Image").GetComponent<Image>().sprite = _slot.PrismiteObject.uiDisplay;
        _slot.slotDisplay.transform.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
      } else {
        _slot.slotDisplay.transform.Find("Image").GetComponent<Image>().sprite = null;
        _slot.slotDisplay.transform.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0);
      }
    }

    public abstract void createSlots();

    protected void addEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action) {
      EventTrigger trigger = obj.GetComponent<EventTrigger>();
      var eventTrigger = new EventTrigger.Entry();
      eventTrigger.eventID = type;
      eventTrigger.callback.AddListener(action);
      trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj) {
      MouseData.slotHoveredOver = obj;
    }

    public void OnExit(GameObject obj) {
      MouseData.slotHoveredOver = null;
    }

    public void OnEnterInterface(GameObject obj) {
      Debug.Log("In Interface: " + obj);
      MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj) {
      MouseData.interfaceMouseIsOver = null;
    }

    public void OnDragStart(GameObject obj) {
      if (slotsOnInterface[obj].prismite.id <= -1) return;
      MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }

    public GameObject CreateTempItem(GameObject obj) {
      GameObject tempItem = null;
      if (slotsOnInterface[obj].prismite.id >= 0) {
        tempItem = new GameObject();
        var rt = tempItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0.5f, 0.5f);
        tempItem.transform.SetParent(transform.parent);

        var img = tempItem.AddComponent<Image>();
        img.sprite = slotsOnInterface[obj].PrismiteObject.uiDisplay;
        img.raycastTarget = false;
      }
      return tempItem;
    }

    public void OnDragEnd(GameObject obj) {
      Destroy(MouseData.tempItemBeingDragged);
      if (MouseData.interfaceMouseIsOver == null) {
        // slotsOnInterface[obj].RemoveItem();
        return;
      }
      if (MouseData.interfaceMouseIsOver.canAcceptItems && MouseData.slotHoveredOver) {
        InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
        inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
      }
    }

    public void OnDrag(GameObject obj) {
      if (MouseData.tempItemBeingDragged != null) {
        Vector3 t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        t.z = 0f;
        MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = t;
      }
    }
}
