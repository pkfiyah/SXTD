using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject> {
  private int width;
  private int height;
  private TGridObject[, ] gridArray;

  public Grid(int width, int height, Func<Grid<TGridObject>, int, int, bool, TGridObject> createGridObject) {
    this.width = width;
    this.height = height;
    gridArray = new TGridObject[width, height];
    for(int x = 0; x < gridArray.GetLength(0); x++) {
      for(int y = 0; y < gridArray.GetLength(1); y++) {
        gridArray[x, y] = createGridObject(this, x, y, true);
      }
    }
  }

  public int getWidth() {
    return width;
  }

  public int getHeight() {
    return height;
  }

  public void setGridObject(int x, int y, TGridObject value) {
    if (x >= 0 && y >= 0 && x < width && y < height) {
      gridArray[x, y] = value;
    }
  }

  public TGridObject getGridObject(int x, int y) {
    if (x >= 0 && y >= 0 && x < width && y < height) {
      return gridArray[x, y];
    } else {
      return default(TGridObject);
    }
  }
}
