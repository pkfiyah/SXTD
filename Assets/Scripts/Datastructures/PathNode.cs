using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {
  private Grid<PathNode> grid;
  private int x;
  private int y;

  public int gCost;
  public int hCost;
  public int fCost;

  public PathNode parent;

  public bool isTraversable;

  public PathNode(Grid<PathNode> g, int x, int y, bool isTraversable) {
    this.isTraversable = isTraversable;
    this.grid = g;
    this.x = x;
    this.y = y;
  }

  public void calculateFCost() {
    fCost = gCost + hCost;
  }
  public override string ToString() {
    return x + ", " + y;
  }

  public int getX() {
    return x;
  }

  public int getY() {
    return y;
  }
}
