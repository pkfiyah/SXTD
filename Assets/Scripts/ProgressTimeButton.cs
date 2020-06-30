using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTimeButton : MonoBehaviour
{
    public RunStateObject runState;

    public void progressTime() {
      if (runState.GetState == State.Planning) {
        runState.ProgressState();
      }
    }
}
