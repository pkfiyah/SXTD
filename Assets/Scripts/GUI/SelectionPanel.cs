using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionPanel : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public Image displayImage;
    // Start is called before the first frame update
    void Awake() {
      damageText.text = "Dam: 0";
      speedText.text = "Spe: 0";
    }

    // Update is called once per frame
    void FixedUpdate() {
      if (MouseData.activeSelection != null) {
        GameboardPiece gp = MouseData.activeSelection.GetComponent<GameboardPiece>();
        damageText.text = "Dam: " + gp.GetPieceDamage;
        speedText.text = "Spe: " + gp.GetPieceAttackSpeed;
        if (gp.piece.tile != null) {
          displayImage.sprite = gp.piece.tile.sprite;
        } else  {
          displayImage.sprite = MouseData.activeSelection.GetComponent<SpriteRenderer>().sprite;
        }
      } else {

      }
    }
}
