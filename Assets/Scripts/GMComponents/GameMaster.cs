 ﻿using System.Collections;
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
  // void OnEnable() {
  //   TDEvents.TimeChange.AddListener(OnTimeChange);
  // }
  //
  // void OnDisable() {
  //     TDEvents.TimeChange.RemoveListener(OnTimeChange);
  // }
  //
  // private void OnTimeChange(int newTime) {
  //   if (newTime == GameClock.DAYTIME_STARTTIME) {
  //     StartCoroutine(CheckStageCompletionParams());
  //   }
  // }

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
    if (clock.IsDaytime) {
      if (clock.Tick(4)) {
        runState.ResetBots();
      } else {
        StartNighttime();
      }
    }
  }

  // Handle nighttime waving
  public void StartNighttime() {
    clock.StartNighttime();
    InvokeRepeating("UpdateNighttime", GameClock.SECONDS_IN_HOUR + GameClock.ACTIVE_START_DELAY_TIME, GameClock.SECONDS_IN_HOUR);
  }

  void UpdateNighttime() {
    clock.Tick();
    if (clock.IsDaytime) {
      CancelInvoke("UpdateNighttime");
    }
  }

  public void EndNighttime() {
    Debug.Log("EndNight Here");
    CancelInvoke("UpdateNighttime");
    int currTime = clock.GetTime();
    int skipHours = 12;
    if (currTime >= GameClock.NIGHTTIME_STARTTIME) skipHours = skipHours - (currTime - GameClock.NIGHTTIME_STARTTIME);
    else skipHours = skipHours - 6 - currTime;
    clock.Tick(skipHours);
    clock.StartDaytime();
    runState.ResetBots();
  }

  public bool MakePurchase(int i) {
    return runState.AssignBots(i);
  }

  public void PlaceGameboardPiece(GameObject gameboardPiece, Vector3Int tilePosition) {
    if (gameboardPiece.GetComponent<SpriteRenderer>() != null) {
      gameboardPiece.GetComponent<SpriteRenderer>().color = Color.white;
    }
    TDEvents.RequestDrone.Invoke(tilePosition);
    Gameboard.Instance.UpdateGameboard(tilePosition, Instantiate(gameboardPiece, tilePosition, Quaternion.identity));
  }

  public void PlaceEntity(GameObject gameboardEntity, Vector3Int tilePosition) {
    Gameboard.Instance.AddEntity(tilePosition, Instantiate(gameboardEntity, tilePosition, Quaternion.identity));
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
