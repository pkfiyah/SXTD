using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

    public WaveObject wave;
    private bool isSpawning = false;

    void OnEnable() {
      TDEvents.IsNightChange.AddListener(BeginWave);
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

      if (isNight && !isSpawning) {
        StartCoroutine(SpawnWaves());
      }
    }

    public bool HasWaves() {
      return isSpawning;
    }

    public void PrepNextNight(WaveObject nextNight) {
      wave = nextNight;
    }

    /* Spawn wave initiates the spawning of a nighttime worth of enemies
     *
     * After The Night start delay time,
     */
    IEnumerator SpawnWaves() {
      int waveIndex = 0;
      isSpawning = true;
      yield return new WaitForSeconds(GameClock.ACTIVE_START_DELAY_TIME);
      while (waveIndex < wave.NumberOfWaves) {
        for (int i = 0; i < wave.EnemyCount(waveIndex); i++) {
          GameMaster.Instance.PlaceEntity(wave.GetEnemy(waveIndex), GetComponent<GameboardPiece>().GetTilePosition());
          yield return new WaitForSeconds(wave.EnemyStaggerTime(waveIndex));
        }
        yield return new WaitForSeconds(wave.WaveStaggerTime(waveIndex));
        waveIndex++;
      }
      isSpawning = false;
    }
}
