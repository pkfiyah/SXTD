using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  public int redColour;
  public int blueColour;
  public int yellowColour;
  public int quality;
  public int cost;
  public PrismiteBuff[] buffs;

  // Clones an existing PrismiteObject
  public Prismite(PrismiteObject item) {
    id = item.data.id;
    redColour = item.data.redColour;
    blueColour = item.data.blueColour;
    yellowColour = item.data.yellowColour;
    quality = item.data.quality;
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
  public float value;
  public float min;
  public float max;
  public PrismiteBuff(float _min, float _max) {
    min = _min;
    max = _max;
    GenerateValue();
  }

  public void GenerateValue() {
    value = UnityEngine.Random.Range(min, max);
  }

  public void AddValue(ref int baseValue) {
    baseValue += (int)Mathf.Floor(value);
  }

  public void RemoveValue(ref int baseValue) {
    baseValue -= (int)Mathf.Floor(value);
  }

  public void AddValue(ref float baseValue) {
    baseValue += value;
  }

  public void RemoveValue(ref float baseValue) {
    baseValue -= value;
  }
}
