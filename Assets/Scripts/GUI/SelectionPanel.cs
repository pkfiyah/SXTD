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
        damageText.text = "Dam: " + MouseData.activeSelection.piece.data.damage.BaseValue;
        speedText.text = "Spe: " + MouseData.activeSelection.piece.data.attackSpeed;
        displayImage.sprite = MouseData.activeSelection.piece.tile.sprite;
      } else {

      }
    }
}
