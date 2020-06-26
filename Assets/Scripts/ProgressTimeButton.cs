using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTimeButton : MonoBehaviour
{
    public GameMaster _gm;



    void Awake() {
    }

    public void progressTime() {
      if (GameMaster.Instance.runState.GetState == State.Planning) {
        GameMaster.Instance.runState.data.progressState();
      }
    }
}
