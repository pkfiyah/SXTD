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
        clock.StartNighttime();
        StartCoroutine(Nighttime());
      }
    }
  }

  // Handle nighttime waving
  public IEnumerator Nighttime() {
    InvokeRepeating("UpdateNighttime", GameClock.SECONDS_IN_HOUR + GameClock.ACTIVE_START_DELAY_TIME, GameClock.SECONDS_IN_HOUR);
    yield return new WaitForSeconds(GameClock.ACTIVE_START_DELAY_TIME);
  }

  void UpdateNighttime() {
    clock.Tick();
    if (clock.IsDaytime) {
      CancelInvoke("UpdateNighttime");
    }
  }

  public void EndNighttime() {
    Debug.Log("EndNight Here");
  }

  public bool MakePurchase(int i) {
    return runState.AssignBots(i);
  }

  public void PlaceGameboardPiece(GameObject gameboardPiece, Vector3Int tilePosition) {
    if (gameboardPiece.GetComponent<SpriteRenderer>() != null) {
      gameboardPiece.GetComponent<SpriteRenderer>().color = Color.white;
    }
    Gameboard.Instance.UpdateGameboard(tilePosition, Instantiate(gameboardPiece, tilePosition, Quaternion.identity));
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
