using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour {
    public GameObject DronePrefab;
    private GameObject[] UnusedDrones;

    void OnEnable() {
      TDEvents.RequestDrone.AddListener(LaunchDrone);
      UnusedDrones = new GameObject[8];
    }

    void OnDisable() {
      TDEvents.RequestDrone.RemoveListener(LaunchDrone);
    }

    private void LaunchDrone(Vector3Int position) {
      Vector3 LaunchPosition = Gameboard.Instance.GetWorldPositionFromTilePosition(Gameboard.Instance.GetHearthTile());
      GameObject newDrone = Instantiate(DronePrefab, LaunchPosition, Quaternion.identity);
      newDrone.GetComponent<DroneBehaviour>().SetPathToTargetPosition(position);
    }
}
