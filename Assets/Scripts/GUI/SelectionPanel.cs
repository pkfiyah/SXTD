using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionPanel : MonoBehaviour
{
    public TextMeshProUGUI selectionName;
    public TextMeshProUGUI damageBaseText;
    public TextMeshProUGUI damageModText;
    public TextMeshProUGUI damageTotalText;
    public TextMeshProUGUI speedBaseText;
    public TextMeshProUGUI speedModText;
    public TextMeshProUGUI speedTotalText;

    public Transform bottomPanelTransform;
    public float slideDistance;

    public Image displayImage;

    // Update is called once per frame
    void FixedUpdate() {
      if (MouseData.activeSelection != null) {
        bottomPanelTransform.localPosition = Vector3.zero;
        GameboardPiece gp = MouseData.activeSelection.GetComponent<GameboardPiece>();
        damageBaseText.text = gp.GetPieceDamage.BaseValue.ToString();
        speedBaseText.text = gp.GetPieceAttackSpeed.BaseValue.ToString();
        damageModText.text = (gp.GetPieceDamage.ModifiedValue - gp.GetPieceDamage.BaseValue).ToString();
        speedModText.text = (gp.GetPieceAttackSpeed.ModifiedValue - gp.GetPieceAttackSpeed.BaseValue).ToString();
        damageTotalText.text = gp.GetPieceDamage.ModifiedValue.ToString();
        speedTotalText.text = gp.GetPieceAttackSpeed.ModifiedValue.ToString();
        selectionName.text = gp.piece.data.name;

        if (gp.piece.tile != null) {
          displayImage.sprite = gp.piece.tile.sprite;
        } else  {
          displayImage.sprite = MouseData.activeSelection.GetComponent<SpriteRenderer>().sprite;
        }
      } else {
        bottomPanelTransform.localPosition = new Vector3(0f, slideDistance, 0f);
      }
    }
}
