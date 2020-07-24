using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

    public WaveObject waves;
    public bool HasWaves { get { return waves.HasWaves; } }

    private bool isNight = false;

    void OnEnable() {
      TDEvents.IsNightChange.AddListener(BeginWave);
      waves.Refresh();
    }

    void OnDisable() {
      TDEvents.IsNightChange.RemoveListener(BeginWave);
    }

    // Wave happens over the entire night
    void BeginWave(bool isNight) {
      this.isNight = isNight;
      if (isNight) {
        StartCoroutine(SpawnWaves());
      }
    }

    /* Spawn wave initiates the spawning of a nighttime worth of enemies
     *
     * After The Night start delay time,
     */
    IEnumerator SpawnWaves() {
      yield return new WaitForSeconds(GameClock.ACTIVE_START_DELAY_TIME);
      while (isNight && waves.HasWaves) {
        for (int i = 0; i < waves.EnemyCount; i++) {
          GameMaster.Instance.PlaceGameboardPiece(waves.GetEnemy, GetComponent<GameboardPiece>().GetTilePosition());
          // GameObject newEnemy = Instantiate(waves.GetEnemy, transform.position, Quaternion.identity);
          // List<Vector3Int> movementPath = Gameboard.Instance.aStar(GetComponent<GameboardPiece>().GetTilePosition());
          // newEnemy.GetComponent<EntityPiece>().SetPathToTargetPosition(movementPath);
          yield return new WaitForSeconds(waves.EnemyStaggerTime);
        }
        yield return new WaitForSeconds(waves.WaveStaggerTime);
        waves.NextWave();
      }
    }
}
