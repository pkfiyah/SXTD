using System.IO;
﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

public enum InterfaceType {
  Inventory,
  Tower,
  Other
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject {

  public string savePath;
  public PrismiteDatabaseObject database; // Reference to all existing prismite
  public InterfaceType type; // All InventoryObjects have an interface
  public Inventory data;  // The actual Inventory data
  public InventorySlot[] GetSlots { get { return data.slots; } } // quick reference to get the Inventory Slots array

  public Inventory CreateInventory() {
    return new Inventory(this);
  }

  public bool AddPrismite(Prismite prismite) {
    if (EmptySlotCount <= 0) return false;
    SetEmptySlot(prismite);
    return true;
  }

  public int EmptySlotCount {
    get {
      int counter = 0;
      for (int i = 0; i < GetSlots.Length; i++) {
        if (GetSlots[i].prismite.id <= -1) counter++;
      }
      return counter;
    }
  }

  public InventorySlot SetEmptySlot(Prismite prismite) {
    for (int i = 0; i < GetSlots.Length; i++) {
      if (GetSlots[i].prismite.id <= -1) {
        GetSlots[i].UpdateSlot(prismite);
        return GetSlots[i];
      }
    }
    // Deal with full inventory here
    return null;
  }

  public void SwapItems(InventorySlot slot1, InventorySlot slot2) {
    InventorySlot temp = new InventorySlot(slot2.prismite);
    slot2.UpdateSlot(slot1.prismite);
    slot1.UpdateSlot(temp.prismite);
  }

  public void RemoveItem(Prismite prismite) {
    for (int i = 0; i < GetSlots.Length; i++) {
      if (GetSlots[i].prismite == prismite) {
        GetSlots[i].UpdateSlot(null);
      }
    }
  }

  [ContextMenu("Save")]
  public void Save() {
    string saveData = JsonUtility.ToJson(this, true);
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
    bf.Serialize(file, saveData);
    file.Close();
  }

  [ContextMenu("Load")]
  public void Load() {
    if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
      JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
      file.Close();
    }
  }

  [ContextMenu("Clear")]
  public void Clean() {
    data.Clear();
  }
}

// -----------------------------------------------------------------------------

[System.Serializable]
public class Inventory {
  public InventorySlot[] slots = new InventorySlot[8];
  // public Inventory(){}
  public Inventory(int slotCount) {
    slots = new InventorySlot[slotCount];
  }

  public Inventory(InventoryObject orig) {
      slots = new InventorySlot[orig.GetSlots.Length];
      for (int i = 0; i < orig.GetSlots.Length; i++) {
        slots[i] = new InventorySlot(orig.GetSlots[i].prismite);
      }
  }

  public void Clear() {
    for (int i = 0; i < slots.Length; i++) {
      slots[i].RemoveItem();
    }
  }
}
