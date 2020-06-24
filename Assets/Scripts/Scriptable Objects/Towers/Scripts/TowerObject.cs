using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType {
  Physical,
  Magic,
  Utility,
  Minion
}

public enum TowerAttributes {
  Power,
  FireRate
}

public abstract class TowerObject : ScriptableObject {

    public Sprite uiDisplay;
    //public TowerInterface inventoryDisplay;
    public TowerType type;
    [TextArea(15,20)]
    public string description;
    public Tower data = new Tower();

    public Tower CreateTower() {
      return new Tower(this);
    }
}

[System.Serializable]
public class Tower {
  public string name;
  public int id = -1;
  private Inventory inventory;
  // public ItemBuff[] buffs;
  public Tower(TowerObject tower) {
    name = tower.data.name;
    id = tower.data.id;
    // buffs = new ItemBuff[item.data.buffs.Length];
    // for (int i = 0; i < buffs.Length; i++) {
    //   buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
    // }
  }

  public Tower() {
    name = "";
    id = -1;
    //inventory = new TowerInventory();
  }
}

// [System.Serializable]
// public class ItemBuff : IModifiers {
//   public Attributes attribute;
//   public int value;
//   public int min;
//   public int max;
//   public ItemBuff(int _min, int _max) {
//     min = _min;
//     max = _max;
//     generateValue();
//   }
//
//   public void generateValue() {
//     value = UnityEngine.Random.Range(min, max);
//   }
//
//   public void AddValue(ref int baseValue) {
//     baseValue += value;
//   }
// }
