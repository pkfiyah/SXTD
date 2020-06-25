using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTower : Piece {

    private List<Prismite> _prismiteNodes;

    public override void Awake() {
      base.Awake();
      _prismiteNodes = new List<Prismite>();
    }

    private void setPrismite(Prismite p) {
      if (_prismiteNodes.Count < 3) _prismiteNodes.Add(p);
    }

    public void slotPrismite(Prismite prismite) {

    }
}
