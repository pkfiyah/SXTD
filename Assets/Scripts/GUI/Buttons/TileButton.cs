using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour {
    public GameObject tilePrefab;
    public GameMaster gm;
    public void tileButtonPressed() {
      if (gm.getActiveSelection() == null) {
        gm.setActiveSelection(tilePrefab);
      } else {
        gm.setActiveSelection(null);
      }
    }
}
