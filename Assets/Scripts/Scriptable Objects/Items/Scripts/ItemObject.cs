using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
  Food,
  Equipment,
  Default
}

public enum Attributes {
  Agi,
  Int,
  Str
}

// ItemObject holds reference to Item
public abstract class ItemObject : ScriptableObject {

    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();

    public Item CreateItem() {
      Item newItem = new Item(this);
      return newItem;
    }
}

[System.Serializable]
public class Item {
  public string name;
  public int id = -1;
  public ItemBuff[] buffs;
  public Item(ItemObject item) {
    name = item.data.name;
    id = item.data.id;
    buffs = new ItemBuff[item.data.buffs.Length];
    for (int i = 0; i < buffs.Length; i++) {
      buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
    }
  }

  public Item() {
    name = "";
    id = -1;
  }
}

[System.Serializable]
public class ItemBuff : IModifiers {
  public Attributes attribute;
  public int value;
  public int min;
  public int max;
  public ItemBuff(int _min, int _max) {
    min = _min;
    max = _max;
    generateValue();
  }

  public void generateValue() {
    value = UnityEngine.Random.Range(min, max);
  }

  public void AddValue(ref int baseValue) {
    baseValue += value;
  }
}
