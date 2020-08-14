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
  public TextMeshProUGUI costText;

  public InventoryObject playerInventory;

  private PrismiteObject prismite;
  private Vector3Int prismiteTilePosition;

  void OnEnable() {
    TDEvents.SelectionChanged.AddListener(OnSelectionChange);
  }

  void OnDisable() {
    TDEvents.SelectionChanged.RemoveListener(OnSelectionChange);
  }

  void OnSelectionChange(GameObject newSelection) {
    GameboardPiece gp = newSelection.GetComponent<GameboardPiece>();
    prismiteTilePosition = gp.GetTilePosition();
    if (gp.piece.data.type == PieceType.Prismite) {
      PrismiteNode pn = newSelection.GetComponent<PrismiteNode>();
      prismite = pn.GetPrismite();
      transform.position = newSelection.transform.position + new Vector3(2.25f, 0, 0);
      prismiteDisplayImage.sprite = prismite.uiDisplay;
      redSlider.value = prismite.data.redColour;
      blueSlider.value = prismite.data.blueColour;
      yellowSlider.value = prismite.data.yellowColour;
      nameText.text = prismite.data.name;
      costText.text = prismite.data.cost.ToString();

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

  public void OnPurchaseButtonClicked() {
    if (GameMaster.Instance.HarvestPrismite(prismiteTilePosition, prismite.data.cost)) {
      Debug.Log("Purchasing");
      Debug.Log("PrismiteData:" + prismite.data);
      Debug.Log("PrismiteDataID:" + prismite.data.id);
      Debug.Log("PrismiteDataID:" + prismite.data.name);
      playerInventory.AddPrismite(prismite.data);
    }
  }
}
