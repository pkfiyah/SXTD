using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrismiteSelectionPanel : MonoBehaviour {

  public Image prismiteDisplayImage;

  void OnEnable() {
    TDEvents.SelectionChanged.AddListener(OnSelectionChange);
  }

  void OnDisable() {
    TDEvents.SelectionChanged.RemoveListener(OnSelectionChange);
  }

  void OnSelectionChange(GameObject newSelection) {
    GameboardPiece gp = newSelection.GetComponent<GameboardPiece>();
    if (gp.piece.data.type == PieceType.Prismite) {
      PrismiteNode pn = newSelection.GetComponent<PrismiteNode>();
      transform.position = newSelection.transform.position /*+ new Vector3(gameObject.GetComponent<RectTransform>().rect.width / 2, 0f, 0f)*/;
      prismiteDisplayImage.sprite = pn.GetPrismite().uiDisplay;
    } else {
      transform.position = new Vector3(-10, -10, 0);
    }
  }
}
