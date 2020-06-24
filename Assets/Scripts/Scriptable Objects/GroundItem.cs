using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundItem : MonoBehaviour {
    public PrismiteObject item;
    public void OnMouseDown() {
      GameMaster.Instance.triggeredThing(item);
      Destroy(gameObject);
    }
}
