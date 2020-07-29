using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableFloat {
  [SerializeField]
  private float baseValue;
  public float BaseValue { get { return baseValue; } set { baseValue = value; } }

  [SerializeField]
  private float modifiedValue;
  public float ModifiedValue { get { return modifiedValue; } private set { modifiedValue = value; } }

  public List<IModifiers> modifiers = new List<IModifiers>();
  public event ModifiedEvent ValueModified;

  public ModifiableFloat(ModifiedEvent method = null ) {
    modifiedValue = BaseValue;
    if (method != null) ValueModified += method;
  }

  public ModifiableFloat(float baseValue) {
    this.baseValue = baseValue;
    modifiedValue = BaseValue;
  }

  public void RegisterModEvent(ModifiedEvent method) {
    ValueModified += method;
  }

  public void UnregisterModEvent(ModifiedEvent method) {
    ValueModified -= method;
  }

  public void UpdateModifiedValue() {
    float valueToAdd = 0f;
    for (int i = 0; i < modifiers.Count; i++) {
      modifiers[i].AddValue(ref valueToAdd);
    }
    ModifiedValue = baseValue + valueToAdd;
    if (ValueModified != null) ValueModified.Invoke();
  }

  public void AddModifier(IModifiers _modifier){
    modifiers.Add(_modifier);
    UpdateModifiedValue();
  }

  public void RemoveModifier(IModifiers _modifier){
    modifiers.Remove(_modifier);
    UpdateModifiedValue();
  }
}
