using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Prismite Database", menuName = "Inventory System/Prismite/Database")]
public class PrismiteDatabaseObject : ScriptableObject, ISerializationCallbackReceiver {

    public PrismiteObject[] prismiteObjects;
    public Dictionary<int, PrismiteObject> GetPrismite = new Dictionary<int, PrismiteObject>(); //Lookup for Prismite by Id

    public void OnAfterDeserialize() {
      for (int i = 0; i < prismiteObjects.Length; i++) {
        prismiteObjects[i].data.id = i;
        GetPrismite.Add(i, prismiteObjects[i]);
      }
    }

    public void OnBeforeSerialize() {
      GetPrismite = new Dictionary<int, PrismiteObject>();
    }
}

// Reference to all Scriptable Prismite Objects that exist. Can get a reference to an PrismiteObject in the database via it's ID
