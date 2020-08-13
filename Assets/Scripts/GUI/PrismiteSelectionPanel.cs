using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrismiteSelectionPanel : MonoBehaviour {

  public Image prismiteDisplayImage;
  public Image[] prismiteQualityIcons;

  public Slider redSlider;
  public Slider blueSlider;
  public Slider yellowSlider;

  public TextMeshProUGUI nameText;
  public TextMeshProUGUI attackText;
  public TextMeshProUGUI defenceText;
  public TextMeshProUGUI speedText;

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
      Debug.Log("newSelection.transform.position: " + newSelection.transform.position);
      transform.position = newSelection.transform.position + new Vector3(2.25f, 0, 0);
      PrismiteObject prismite = pn.GetPrismite();
      prismiteDisplayImage.sprite = prismite.uiDisplay;
      redSlider.value = prismite.data.redColour;
      blueSlider.value = prismite.data.blueColour;
      yellowSlider.value = prismite.data.yellowColour;
      nameText.text = prismite.data.name;

      for (int i = 0; i < prismite.data.quality; i++) {
        prismiteQualityIcons[i].enabled = true;
      }

    } else {
      transform.position = new Vector3(-10, -10, 0);
      for (int i = 0; i < prismiteQualityIcons.Length; i++) {
        prismiteQualityIcons[i].enabled = false;
      }
    }
  }

  void OnClose() {

  }

  void OnHarvest () {

  }
}
