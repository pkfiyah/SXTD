using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismiteCasino : MonoBehaviour {

    public InventoryObject inventory;

    private int lastTime;

    void Awake() {
      GeneratePrismite();
    }

    void OnEnable() {
      TDEvents.TimeChange.AddListener(RollPrismite);
    }

    void OnDisable() {
      TDEvents.TimeChange.RemoveListener(RollPrismite);
    }

    public void RollPrismite(int newTime) {
      if (lastTime == newTime || GameClock.HasNightStarted) return;
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
