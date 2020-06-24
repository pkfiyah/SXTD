using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All Prismite Colours
public enum PrismiteColour {
  Red,
  Blue,
  Yellow,
  Green,
  Orange,
  Purple,
  White,
  Black
}

// All Prismite Modifiers
public enum PrismiteModifier {
  Damage,
  CriticalRate,
  FireRate
}

// PrismiteObject holds reference to Prismite + other references
[CreateAssetMenu(fileName = "New Prismite Object", menuName = "Inventory System/Prismite/New Prismite")]
public class PrismiteObject : ScriptableObject {

    public Sprite uiDisplay; // Sprite shown in inventories
    [TextArea(15,20)]
    public string description;
    public Prismite data = new Prismite();

    public Prismite CreatePrismite() {
      return new Prismite(this);
    }
}

//    --------------------------------------------------------------------------

[System.Serializable]
public class Prismite {

  public string name;
  public int id = -1;
  public PrismiteColour colour;
  public PrismiteBuff[] buffs;

  // Clones an existing PrismiteObject
  public Prismite(PrismiteObject item) {
    id = item.data.id;
    buffs = new PrismiteBuff[item.data.buffs.Length];
    for (int i = 0; i < buffs.Length; i++) {
      buffs[i] = new PrismiteBuff(item.data.buffs[i].min, item.data.buffs[i].max);
    }
  }

  // Default Prismite
  public Prismite() {
    name = "";
    id = -1;
  }
}

// -----------------------------------------------------------------------------

[System.Serializable]
public class PrismiteBuff : IModifiers {
  public PrismiteModifier modifier;
  public int value;
  public int min;
  public int max;
  public PrismiteBuff(int _min, int _max) {
    min = _min;
    max = _max;
    GenerateValue();
  }

  public void GenerateValue() {
    value = UnityEngine.Random.Range(min, max);
  }

  public void AddValue(ref int baseValue) {
    baseValue += value;
  }
}
