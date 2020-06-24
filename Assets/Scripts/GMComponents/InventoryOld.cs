using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOld {

    private static int INVENTORY_SLOT_COUNT = 8;

    private Prismite[] prismiteSlots;

    public event EventHandler onPrismiteListChange;

    public InventoryOld() {
      prismiteSlots = new Prismite[INVENTORY_SLOT_COUNT];
      // addPrismite(0, new Prismite { prismiteType = Prismite.PrismiteType.Green, prismiteQuality = Prismite.PrismiteQuality.Smooth });
      // addPrismite(new Prismite { prismiteType = Prismite.PrismiteType.Blue, prismiteQuality = Prismite.PrismiteQuality.Smooth });
      // addPrismite(new Prismite { prismiteType = Prismite.PrismiteType.White, prismiteQuality = Prismite.PrismiteQuality.Smooth });
      // addPrismite(null);
    }

    public void addPrismite(Prismite prismite) {
      for (int i = 0; i < prismiteSlots.Length; i++) {
        if (prismiteSlots[i] == null) {
          prismiteSlots[i] = prismite;
          onPrismiteListChange?.Invoke(this, EventArgs.Empty);
          return;
        }
      }
      Debug.Log("no space");
    }

    public void addPrismite(int index, Prismite prismite) {
      if (prismiteSlots[index] == null) {
        prismiteSlots[index] = prismite;
        onPrismiteListChange?.Invoke(this, EventArgs.Empty);
      }
    }

    public void removePrismite(int index, Prismite prismite) {
      prismiteSlots[index] = null;
      onPrismiteListChange?.Invoke(this, EventArgs.Empty);
    }

    public Prismite[] getPrismiteList() {
      return prismiteSlots;
    }
}
