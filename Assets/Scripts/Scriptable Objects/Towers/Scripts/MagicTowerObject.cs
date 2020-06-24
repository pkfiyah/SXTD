using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Object", menuName = "Inventory System/Towers/Magic Tower")]
public class MagicTowerObject : TowerObject {
    public void Awake() {
      type = TowerType.Magic;
    }  
}
