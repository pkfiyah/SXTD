using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTimeButton : MonoBehaviour
{
    public GameMaster _gm;

    

    void Awake() {
    }

    public void progressTime() {
      if (_gm.isPlanning) {
        _gm.isPlanning = false;
      }
    }
}
