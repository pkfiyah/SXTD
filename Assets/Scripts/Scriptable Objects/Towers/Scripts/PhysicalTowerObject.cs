using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Object", menuName = "Inventory System/Towers/Physical Tower")]
public class PhysicalTowerObject : TowerObject {
  public void Awake() {
    type = TowerType.Physical;
  }
}
