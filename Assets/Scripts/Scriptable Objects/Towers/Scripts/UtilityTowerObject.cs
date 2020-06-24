using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Object", menuName = "Inventory System/Towers/Utility Tower")]
public class UtilityTowerObject : TowerObject {
  public void Awake() {
    type = TowerType.Utility;
  }
}
