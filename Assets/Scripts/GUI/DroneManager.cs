using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecallDronesEvent : UnityEvent { }

public class DroneManager : MonoBehaviour {
    public GameObject DronePrefab;
    private GameObject[] UnusedDrones;
    private RecallDronesEvent RecallDrones = new RecallDronesEvent();
    private int droneIndex = 0;

    void InitDrones() {
      UnusedDrones = new GameObject[4];
      for (int i = 0; i < 4; i++) {
        Vector3 LaunchPosition = Gameboard.Instance.GetHearthTile();
        UnusedDrones[i] = Instantiate(DronePrefab, LaunchPosition, Quaternion.identity);
        RecallDrones.AddListener(UnusedDrones[i].GetComponent<DroneBehaviour>().ListenToRecall);
        UnusedDrones[i].transform.parent = transform;
      }
    }

    void OnEnable() {
      TDEvents.RequestDrone.AddListener(LaunchDrone);
      TDEvents.TimeChange.AddListener(OnTimeChange);
      // for (int i = 0; i < 4; i++) {
      //   RecallDrones.AddListener(UnusedDrones[i].GetComponent<DroneBehaviour>().ListenToRecall);
      // }
    }

    private void OnTimeChange(int meh) {
      RecallDrones.Invoke();
      droneIndex = 0;
    }

    void OnDisable() {
      TDEvents.RequestDrone.RemoveListener(LaunchDrone);
      TDEvents.TimeChange.RemoveListener(OnTimeChange);
      for (int i = 0; i < 4; i++) {
        RecallDrones.RemoveListener(UnusedDrones[i].GetComponent<DroneBehaviour>().ListenToRecall);
      }
    }

    private void LaunchDrone(Vector3Int position) {
      if (UnusedDrones == null) InitDrones();
      if (droneIndex < UnusedDrones.Length) {
         UnusedDrones[droneIndex].GetComponent<DroneBehaviour>().SetPathToTargetPosition(Gameboard.Instance.GetWorldPositionFromTilePosition(position));
         droneIndex++;
      }
    }
}
