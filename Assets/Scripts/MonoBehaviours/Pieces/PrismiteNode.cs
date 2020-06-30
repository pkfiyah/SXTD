using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismiteNode : GameboardPiece {
    // Start is called before the first frame update
    // private InventoryObject casinoInventory;
    private PrismiteObject currentPrismite;

    public PrismiteDatabaseObject database;

    void Awake() {
      NextPrismite();
    }

    private void NextPrismite() {
      currentPrismite = Instantiate(database.GetPrismite[Random.Range(0, database.GetPrismite.Count)]);
      piece.tile.color = GetColourFromPrismite(currentPrismite.data);
    }

    private Color GetColourFromPrismite(Prismite p) {
      switch(p.colour) {
        case PrismiteColour.Red:    return Color.red;
        case PrismiteColour.Blue:   return Color.blue;
        case PrismiteColour.Yellow: return Color.yellow;
        case PrismiteColour.Green:  return Color.green;
        case PrismiteColour.Orange: return new Color(1f, 1f, 0f, 1f);
        case PrismiteColour.Purple: return Color.magenta;
        case PrismiteColour.White:  return Color.white;
        case PrismiteColour.Black:  return Color.black;
        default:
          return Color.white;
      }
    }
}
