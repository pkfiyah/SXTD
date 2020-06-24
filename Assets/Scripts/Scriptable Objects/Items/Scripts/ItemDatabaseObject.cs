using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver {

    public ItemObject[] itemObjects;
    public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize() {
      for (int i = 0; i < itemObjects.Length; i++) {
        itemObjects[i].data.id = i;
        getItem.Add(i, itemObjects[i]);
      }
    }

    public void OnBeforeSerialize() {
      getItem = new Dictionary<int, ItemObject>();
    }
}

// Reference to all Scriptable Item Objects that exist. Can get a reference to an ItemObject in the database via it's ID
// (Assigned at runtime to all ItemObjects at runtime)
