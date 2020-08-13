using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HeroConstructionCard : MonoBehaviour {
  public PieceObject constructablePiece;

  public TextMeshProUGUI baseAttackDamageText;
  public TextMeshProUGUI baseAttackSpeedText;
  public TextMeshProUGUI baseAttackRangeText;

  public Image constructionImage;

  void Awake() {
    constructionImage.sprite = constructablePiece.staticSprite;
    baseAttackDamageText.text = constructablePiece.data.baseDamage.ToString();
    baseAttackSpeedText.text = constructablePiece.data.baseAttackSpeed.ToString();
    baseAttackRangeText.text = constructablePiece.data.baseRange.ToString();
  }
}
