using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour {

    private float _spawnTimer = 0.0f;
    private float _spawnInterval = 2f;
    public List<GameObject> _spawnList = new List<GameObject>();

    void FixedUpdate() {
      if (GameMaster.Instance.runState.GetState == State.Active) {
        _spawnTimer += Time.deltaTime;
        if (_spawnList.Count > 0 && _spawnTimer >= _spawnInterval) {
          _spawnTimer = 0f;
          GameObject piece = Instantiate(_spawnList[0], transform.position, Quaternion.identity);
          _spawnList.RemoveAt(0);

          Gameboard.Instance.UpdateGameboard(Gameboard.Instance.GetTilePositionFromWorldPosition(transform.position), piece);
        }
      }
    }
}
