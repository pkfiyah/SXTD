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
        PieceObject po = MouseData.activeSelection.GetComponent<GameboardPiece>().piece;
        damageText.text = "Dam: " + po.data.damage.BaseValue;
        speedText.text = "Spe: " + po.data.attackSpeed;
        if (po.tile != null) {
          displayImage.sprite = po.tile.sprite;
        } else  {
          displayImage.sprite = MouseData.activeSelection.GetComponent<SpriteRenderer>().sprite;
        }
      } else {

      }
    }
}
