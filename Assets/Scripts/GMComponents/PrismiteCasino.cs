using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismiteCasino : MonoBehaviour {

    public InventoryObject inventory;

    void Awake() {
      GeneratePrismite();
    }

    public void RollPrismite() {
      TDEvents.PrismiteRolled.Invoke();
      GeneratePrismite();
    }

    private void GeneratePrismite() {
      inventory.Clean();
      for (int i = 0; i < inventory.GetSlots.Length; i++) {
        int p = Random.Range(0, inventory.database.prismiteObjects.Length);
        Prismite pris = inventory.database.GetPrismite[p].CreatePrismite();
        inventory.AddPrismite(pris);
      }
    }

    private void OnApplicationQuit() {
      inventory.Clean();
    }
}
