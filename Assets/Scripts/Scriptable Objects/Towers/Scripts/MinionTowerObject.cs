using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Object", menuName = "Inventory System/Towers/Minion Tower")]
public class MinionTowerObject : TowerObject {
    public void Awake() {
      type = TowerType.Minion;
    }
}
