using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Entity Object", menuName = "Inventory System/Entity/New Entity")]
public class EntityObject : ScriptableObject {
  public Entity data = new Entity();

}

[System.Serializable]
public class Entity {
  public int baseHealth;
  public int baseDamage;
  public float baseAttackSpeed;
  public int baseRange;

  public Entity() {
    baseHealth = 100;
    baseDamage = 0;
    baseAttackSpeed = 0f;
    baseRange = 0;
  }

  public Entity(EntityObject po) {
    baseHealth = po.data.baseHealth;
    baseDamage = po.data.baseDamage;
    baseAttackSpeed = po.data.baseAttackSpeed;
    baseRange = po.data.baseRange;
  }
}
