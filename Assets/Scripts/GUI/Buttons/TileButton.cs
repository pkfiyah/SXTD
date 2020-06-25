using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour {
    public GameObject tilePrefab;
    public GameMaster gm;
    public void tileButtonPressed() {
      if (MouseData.activeSelection == null) {
        MouseData.activeSelection = tilePrefab;
      } else {
        MouseData.activeSelection = null;
      }
    }
}
