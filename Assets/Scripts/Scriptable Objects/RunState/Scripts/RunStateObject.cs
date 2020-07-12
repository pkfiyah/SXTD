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
  public int GetBots { get { return data.currentBots; } }
  public RunState data = new RunState();

  void OnEnable() {
    TDEvents.TimeChange.AddListener(UpdateStateTime);
  }

  void OnDisable() {
    TDEvents.TimeChange.RemoveListener(UpdateStateTime);
  }

  public void Reinitialize() {
    data = new RunState();
  }

  private void UpdateStateTime(int time) {
    data.time = time;
  }

  public RunState CreateRunState() {
    return new RunState(this);
  }

  public void ProgressState() {
    TDEvents.BeforeStateChange.Invoke(data.state);
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
    TDEvents.AfterStateChange.Invoke(data.state);
  }

  public bool AssignBots(int botsAssigned) {
    if (data.currentBots - botsAssigned < 0) return false;
    data.currentBots -= botsAssigned;
    TDEvents.CurrencyChange.Invoke(data.currentBots);
    return true;
  }

  public void ResetBots() {
    data.currentBots = data.maxBots;
    TDEvents.CurrencyChange.Invoke(data.maxBots);
  }
}

[System.Serializable]
public class RunState {
  public State state = State.Planning;
  public int currentBots;
  public int maxBots;
  public int stage;
  public int time;

  public RunState(RunStateObject rsObj) {
    state = rsObj.data.state;
    currentBots = rsObj.data.currentBots;
    maxBots = rsObj.data.maxBots;
    stage = rsObj.data.stage;
    time = rsObj.data.time;
  }

  public RunState() {
    state = State.Planning;
    time = 6;
    maxBots = 4;
    currentBots = maxBots;
    stage = 1;
  }
}
