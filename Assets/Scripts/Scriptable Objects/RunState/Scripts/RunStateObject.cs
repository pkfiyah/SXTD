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
  public RunState data = new RunState();

  public RunState CreateRunState() {
    return new RunState(this);
  }
}

public class RunState {
  public State state = State.Planning;
  public int currency = 0;
  // items/modifiers go here
  public int stage = 1;
  public RunState(RunStateObject rsObj) {

  }

  public RunState() {
    state = State.Planning;
  }
}
