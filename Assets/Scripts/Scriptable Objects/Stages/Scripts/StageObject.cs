using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PrismiteObject holds reference to Prismite + other references
[CreateAssetMenu(fileName = "New Stage Object", menuName = "Inventory System/Stage/New Stage")]
public class StageObject : ScriptableObject {
    public List<Rect> UnstableGroundAreas;
    public List<WaveObject> SpawnPointWaves;
    public PrismiteDatabaseObject PrismitePool;
    public Stage data;
    public int StageWidth { get { return data.stageWidth; } private set {} }
    public int StageLength { get { return data.stageLength; } private set {} }
}

[System.Serializable]
public class Stage {
  public int stageWidth;
  public int stageLength;
  public int nightsInStage;
}
