using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseEntity {
  // void takePhysDamage(float damageTaken);
  // void takeMagDamage(float damageTaken);
  // void takePureDamage(float damageTaken);
  Vector3Int GetTilePosition();
  Vector3 GetWorldPosition();
  // bool checkAlive();
  bool IsTraversable();
  // bool isAttackable();
  // bool isSlottable();
  void DestroySelf();
  // void slotPrismite(Prismite prismite);
}
