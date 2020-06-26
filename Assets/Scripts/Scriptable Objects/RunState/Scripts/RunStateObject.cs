using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
  Active,
  Planning,
  Paused,
  Start,
  Default
}

[CreateAssetMenu(fileName = "New Run State Object", menuName = "Inventory System/Run State/New Run State")]
public class RunStateObject : ScriptableObject {
  public string name;
  [TextArea(15,20)]
  public string description;
  public State GetState { get { return data.state; } }
  public RunState data = new RunState();
  public RunState CreateRunState() {
    return new RunState(this);
  }
}

[System.Serializable]
public class RunState {

  public State state = State.Planning;
  public int currency = 0;
  public int stage = 1;

  public RunState(RunStateObject rsObj) {
    state = rsObj.data.state;
    currency = rsObj.data.currency;
    stage = rsObj.data.stage;
  }

  public RunState() {
    state = State.Planning;
  }

  public void progressState() {
    switch (state) {
      case State.Planning:
        state = State.Active;
        break;
      case State.Active:
        state = State.Planning;
        break;
      default:
        break;
    }
  }
}
