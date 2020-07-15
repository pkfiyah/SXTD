using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAssets : MonoBehaviour {
  private static ProjectileAssets _i;

  public static ProjectileAssets Instance {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<ProjectileAssets>("ProjectileAssets"));
      return _i;
    }
  }

  public Transform baseRailbowProjectile;
}
