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

  void Awake() {
    Instance = this;
  }

  // Resets inventory scriptable object on close
  private void OnApplicationQuit() {
    runState.data.state = State.Planning;
    inventory.Clean();
  }

  // Update is called once per frame
  void FixedUpdate() {
    // Planning phase
    if(runState.GetState == State.Planning) {
      if(MouseData.activeSelection != null) {

      }
    }

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
