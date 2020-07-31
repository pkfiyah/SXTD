using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

    public WaveObject wave;

    private int currentWave = 0;
    private bool isNight = false;

    void OnEnable() {
      TDEvents.IsNightChange.AddListener(BeginWave);
      currentWave = 0;
    }

    void OnDisable() {
      TDEvents.IsNightChange.RemoveListener(BeginWave);
    }

    // Wave happens over the entire night
    void BeginWave(bool isNight) {
      if (wave == null) {
        Debug.LogError("Wave has not been set in WaveBehaviour but is trying to spawn.", wave);
        return;
      }
      this.isNight = isNight;
      if (isNight && currentWave < wave.NumberOfWaves) {
        StartCoroutine(SpawnWaves());
      }
    }

    public bool HasWaves() {
      if (currentWave >= wave.NumberOfWaves) return false;
      return true;
    }

    public void PrepNextNight(WaveObject nextNight) {
      wave = nextNight;
      currentWave = 0;
    }

    /* Spawn wave initiates the spawning of a nighttime worth of enemies
     *
     * After The Night start delay time,
     */
    IEnumerator SpawnWaves() {
      yield return new WaitForSeconds(GameClock.ACTIVE_START_DELAY_TIME);
      while (isNight && currentWave < wave.NumberOfWaves) {
        for (int i = 0; i < wave.EnemyCount(currentWave); i++) {
          GameMaster.Instance.PlaceGameboardPiece(wave.GetEnemy(currentWave), GetComponent<GameboardPiece>().GetTilePosition());
          yield return new WaitForSeconds(wave.EnemyStaggerTime(currentWave));
        }
        yield return new WaitForSeconds(wave.WaveStaggerTime(currentWave));
        currentWave++;
      }
    }
}
