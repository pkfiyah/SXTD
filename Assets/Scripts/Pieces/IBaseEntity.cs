using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseEntity {
  void takePhysDamage(float damageTaken);
  void takeMagDamage(float damageTaken);
  void takePureDamage(float damageTaken);
  Vector3Int getTilePosition();
  Vector3 getWorldPosition();
  bool checkAlive();
  bool isTraversable();
  bool isAttackable();
  bool isSlottable();
  void destroySelf();
  void slotPrismite(Prismite prismite);
}
