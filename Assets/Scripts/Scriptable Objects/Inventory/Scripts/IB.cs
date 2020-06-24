// using System.IO;
// ﻿using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.Serialization.Formatters.Binary;
// using UnityEngine;
// using UnityEditor;
//
// public enum InterfaceType {
//   Inventory,
//   Piece,
//   Other
// }
//
// [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
// public class InventoryObject : ScriptableObject {
//
//   public string savePath;
//   public PrismiteDatabaseObject database; // Reference to all existing prismite
//   public InterfaceType type; // All InventoryObjects have an interface
//   public Inventory data;  // The actual Inventory data
//   public InventorySlot[] GetSlots { get { return data.slots; } }
//
//   public bool addItem(Item item, int amt) {
//     if (EmptySlotCount <= 0) return false;
//     InventorySlot slot = FindItemOnInventory(item);
//     if (slot == null) {
//       setEmptySlot(item, amt);
//       return true;
//     }
//     return true;
//   }
//
//   public int EmptySlotCount {
//     get {
//       int counter = 0;
//       for (int i = 0; i < GetSlots.Length; i++) {
//         if (GetSlots[i].item.id <= -1) counter++;
//       }
//       return counter;
//     }
//   }
//
//   public InventorySlot FindItemOnInventory(Item item) {
//     for (int i = 0; i < GetSlots.Length; i++) {
//       if (GetSlots[i].item.id == item.id) return GetSlots[i];
//     }
//     return null;
//   }
//
//   public InventorySlot setEmptySlot(Item item, int amount) {
//     for (int i = 0; i < GetSlots.Length; i++) {
//       if (GetSlots[i].item.id <= -1) {
//         GetSlots[i].updateSlot(item, amount);
//         return GetSlots[i];
//       }
//     }
//     // Deal with full inventory here
//     return null;
//   }
//
//   public void SwapItems(InventorySlot item1, InventorySlot item2) {
//     if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject)) {
//       InventorySlot temp = new InventorySlot(item2.item, item2.amount);
//       item2.updateSlot(item1.item, item1.amount);
//       item1.updateSlot(temp.item, temp.amount);
//     }
//   }
//
//   public void removeItem(Item item) {
//     for (int i = 0; i < GetSlots.Length; i++) {
//       if (GetSlots[i].item == item) {
//         GetSlots[i].updateSlot(null, 0);
//       }
//     }
//   }
//
//   [ContextMenu("Save")]
//   public void save() {
//     string saveData = JsonUtility.ToJson(this, true);
//     BinaryFormatter bf = new BinaryFormatter();
//     FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
//     bf.Serialize(file, saveData);
//     file.Close();
//   }
//
//   [ContextMenu("Load")]
//   public void Load() {
//     if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
//       BinaryFormatter bf = new BinaryFormatter();
//       FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
//       JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
//       file.Close();
//     }
//   }
//
//   [ContextMenu("Clear")]
//   public void Clear() {
//     data.Clear();
//   }
// }
//
// [System.Serializable]
// public class Inventory {
//   public InventorySlot[] slots = new InventorySlot[8];
//   public void Clear() {
//     for (int i = 0; i < slots.Length; i++) {
//       slots[i].RemoveItem();
//     }
//   }
// }
//
// public class TowerInventory : Inventory {
//
//   public TowerInventory() {
//     slots = new InventorySlot[3];
//     for (int i = 0; i < slots.Length; i++) {
//       slots[i] = new InventorySlot();
//     }
//   }
//
//   public void Clear() {
//     for (int i = 0; i < slots.Length; i++) {
//       slots[i].RemoveItem();
//     }
//   }
// }
//
// public delegate void SlotUpdated(InventorySlot slot);
//
// [System.Serializable]
// public class InventorySlot {
//
//   [System.NonSerialized]
//   public UserInterface parent;
//   [System.NonSerialized]
//   public GameObject slotDisplay;
//   [System.NonSerialized]
//   public SlotUpdated OnBeforeUpdate;
//   [System.NonSerialized]
//   public SlotUpdated OnAfterUpdate;
//
//   // public ItemType[] allowedItems = new ItemType[0];
//   public Item item;
//   public int amount;
//
//   public ItemObject ItemObject {
//     get {
//       if (item.id >= 0) { // This is the getter for the item the inventory slot is holding [Ensures it's a valid item in the database]
//         // return parent.inventory.database.GetPrismite[item.id]; FIX IF NEEDED
//       }
//       return null;
//     }
//   }
//
//   public InventorySlot() {
//     updateSlot(new Item(), 0);
//   }
//
//   public InventorySlot(Item _item, int _amt) {
//     updateSlot(_item, _amt);
//   }
//
//   public void updateSlot(Item _item, int _amt) {
//     if (OnBeforeUpdate != null) {
//       OnBeforeUpdate.Invoke(this);
//     }
//
//     item = _item;
//     amount = _amt;
//
//     if (OnAfterUpdate != null) {
//       OnAfterUpdate.Invoke(this);
//     }
//   }
//
//   public void RemoveItem() {
//     updateSlot(new Item(), 0);
//   }
//
//   public void addAmount(int value) {
//     updateSlot(item, amount + value);
//   }
//
//   public bool CanPlaceInSlot(ItemObject itemObject) {
//     if (allowedItems.Length <= 0 || itemObject == null || itemObject.data.id < 0) return true;
//     return false;
//   }
// }
