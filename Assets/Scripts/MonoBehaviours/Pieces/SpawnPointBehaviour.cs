using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour {

    private float _spawnTimer = 0.0f;
    private float _spawnInterval = 2f;
    public List<PieceObject> _spawnList = new List<PieceObject>();

    void Awake() {

    }

    void FixedUpdate() {
      if (GameMaster.Instance.runState.GetState == State.Active) {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval) {
          _spawnTimer = 0f;
          PieceObject piece = _spawnList[0];
          _spawnList.RemoveAt(0);
          Gameboard.Instance.UpdateGameboard(Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position), piece);
          Debug.Log("Spawned");
        }
      }
    }
}
