using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Object", menuName = "Inventory System/Wave/New Wave")]
public class WaveObject : ScriptableObject {

  public List<GameObject> enemies;
  public List<Wave> data;

  public int NumberOfWaves { get { return data.Count; } }

  public GameObject GetEnemy(int waveIndex) {
    return enemies[waveIndex];
  }

  public int EnemyCount(int waveIndex) {
    return data[waveIndex].enemyCount;
  }

  public float WaveStaggerTime(int waveIndex) {
    return data[waveIndex].waveStagger;
  }

  public float EnemyStaggerTime(int waveIndex) {
    return data[waveIndex].enemyStagger;
  }

  // public float TotalWaveTime { get { return data[index].enemyStagger * data[index].enemyCount + data[index].waveStagger; } }
}

[System.Serializable]
public class Wave {
  public int enemyCount;
  public float waveStagger; // Longer break after stagger
  public float enemyStagger; // shorter break between individual enemies

  public Wave() {
    enemyCount = 6;
    waveStagger = 4f;
    enemyStagger = 0.5f;
  }

  public Wave(WaveObject w) {
    enemyCount = w.data[0].enemyCount;
    waveStagger = w.data[0].waveStagger;
  }
}
