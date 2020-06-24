using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseProjectile {
  void getStats();
  void setStats();
  void setTarget(GameObject target);
  void updateProjectileMovement();
}
