using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Object", menuName = "Inventory System/Wave/New Wave")]
public class WaveObject : ScriptableObject {

  private int index = 0;

  public List<GameObject> enemies;
  public List<Wave> data;
  public int NumberOfWaves { get { return data.Count; } }
  public int EnemyCount { get { return data[index].enemyCount; } }
  public GameObject GetEnemy { get { return enemies[index]; } }
  public float WaveStaggerTime { get { return data[index].waveStagger; } }
  public float EnemyStaggerTime { get { return data[index].enemyStagger; } }
  public float TotalWaveTime { get { return data[index].enemyStagger * data[index].enemyCount + data[index].waveStagger; } }
  public bool HasWaves { get { return index + 1 <= NumberOfWaves; } }
  public void Refresh() {
      index = 0;
  }

  public void NextWave() {
    index++;
  }
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
