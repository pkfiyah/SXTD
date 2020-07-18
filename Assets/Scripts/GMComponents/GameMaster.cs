using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using DataStructures.PriorityQueue;

public class GameMaster : MonoBehaviour {
  public InventoryObject inventory;

  // Public Static Reference to the GameMaster from anywhere
  public static GameMaster Instance { get; private set; }
  public RunStateObject runState;

  private GameClock clock;
  private float _activeHourLength = 20f;
  private float _timer = 0f;

  void OnEnable() {
    TDEvents.TimeChange.AddListener(OnTimeChange);
  }

  void OnDisable() {
      TDEvents.TimeChange.RemoveListener(OnTimeChange);
  }

  void Awake() {
    Instance = this;
    clock = new GameClock(runState.data.time);
    runState.ResetBots();
  }

  //void EnterPlanningPhase()

  // Resets inventory scriptable object on close
  private void OnApplicationQuit() {
    runState.Reinitialize();
    inventory.Clean();
  }

  public void ProgressTimeByButton() {
    runState.ResetBots();
    if (!clock.Tick(4) && clock.IsDaytime) {
      clock.StartNighttime();
      StartCoroutine(Nighttime());
    }
  }

  public void OnTimeChange(int newTime) {
    if (runState.GetState == State.Planning && newTime == GameClock.NIGHTTIME_STARTTIME) {
      // switch button to go here, next click will be to start nighttime
      Debug.Log("Nighttime lockdown");
    }
  }

  // Handle nighttime waving
  public IEnumerator Nighttime() {
    InvokeRepeating("UpdateNighttime", GameClock.SECONDS_IN_HOUR + GameClock.ACTIVE_START_DELAY_TIME, GameClock.SECONDS_IN_HOUR);
    yield return new WaitForSeconds(GameClock.ACTIVE_START_DELAY_TIME);
    StateChangeRequest(State.Active);
    Debug.Log("State Progressed to: " + State.Active);
  }

  void UpdateNighttime() {
    clock.Tick();
    if (clock.IsDaytime) {
      StateChangeRequest(State.Planning);
      CancelInvoke("UpdateNighttime");
    }
  }

  public void StateChangeRequest(State newState) {
    runState.ProgressState();
  }

  public bool MakePurchase(int i) {
    return runState.AssignBots(i);
  }

  public void PlaceGameboardPiece(GameObject gameboardPiece, Vector3Int position) {
    if (gameboardPiece.GetComponent<GameboardPiece>().piece.data.type == PieceType.Entity) {
      gameboardPiece.transform.position = position;
    } else {
      if (gameboardPiece.GetComponent<SpriteRenderer>() != null) {
        gameboardPiece.GetComponent<SpriteRenderer>().color = Color.white;
      }
      Gameboard.Instance.UpdateGameboard(position, Instantiate(gameboardPiece, position, Quaternion.identity));
    }
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Debug.Log("Saving");
      inventory.Save();
    }
    if (Input.GetKeyDown(KeyCode.Delete)) {
      Debug.Log(":Loading");
      inventory.Load();
    }
  }
}
