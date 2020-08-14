using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HeroConstructionCard : MonoBehaviour, ISelectHandler, IDeselectHandler {
  public GameObject constructablePiece;

  public TextMeshProUGUI baseAttackDamageText;
  public TextMeshProUGUI baseAttackSpeedText;
  public TextMeshProUGUI baseAttackRangeText;
  public TextMeshProUGUI costText;

  public Image constructionImage;

  void Awake() {
    PieceObject pieceData = constructablePiece.GetComponent<GameboardPiece>().piece;
    constructionImage.sprite = pieceData.staticSprite;
    baseAttackDamageText.text = pieceData.data.baseDamage.ToString();
    baseAttackSpeedText.text = pieceData.data.baseAttackSpeed.ToString();
    baseAttackRangeText.text = pieceData.data.baseRange.ToString();
    costText.text = pieceData.data.cost.ToString();
  }

  void OnEnable() {

  }

  void OnDisable() {

  }

  public void OnSelect(BaseEventData data) {
    TDEvents.RequestConstruction.Invoke(constructablePiece);
  }

  public void OnDeselect(BaseEventData data) {
    TDEvents.RequestConstruction.Invoke(null);
  }
}
