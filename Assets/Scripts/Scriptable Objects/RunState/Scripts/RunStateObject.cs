using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum State {
  Active,
  Planning,
  Paused,
  Start,
  Default
}

[CreateAssetMenu(fileName = "New Run State Object", menuName = "Inventory System/Run State/New Run State")]
public class RunStateObject : ScriptableObject {

  public State GetState { get { return data.state; } }
  public RunState data = new RunState();
  public RunState CreateRunState() {
    return new RunState(this);
  }

  public void ProgressState() {
    if (TDEvents.BeforeStateChange != null) TDEvents.BeforeStateChange.Invoke(data.state);
    switch(GetState) {
      case State.Planning:
        data.state = State.Active;
        break;
      case State.Active:
        data.state = State.Planning;
        break;
      default:
        data.state = State.Planning;
        break;
    }

    if (TDEvents.AfterStateChange != null) TDEvents.AfterStateChange.Invoke(data.state);
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
}
