using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {
  private const int MOVE_STRAIGHT_COST = 10;
  private const int MOVE_DIAGONAL_COST = 14;

  public static Pathfinding Instance { get; private set; }

  private Grid<PathNode> grid;
  private List<PathNode> openList;
  private List<PathNode> closedList;

  public Pathfinding(int width, int height) {
    grid = new Grid<PathNode>(width, height, (Grid<PathNode> g, int x, int y, bool b) => new PathNode(g, x, y, b));
  }

  public void parseGameBoard(IBaseEntity[,] gb) {
    // Parse existing game board traversable into PathNodes for algo
    for(int x = 0; x < gb.GetLength(0); x++) {
      for(int y = 0; y < gb.GetLength(1); y++) {
        grid.setGridObject(x, y, new PathNode(grid, x, y, gb[x, y].IsTraversable()));
      }
    }
    Debug.Log("Game Board Parsed");
  }

  public List<PathNode> findPath(int startX, int startY, int endX, int endY) {
    PathNode startNode = grid.getGridObject(startX, startY);
    PathNode endNode = grid.getGridObject(endX, endY);
    openList = new List<PathNode>();
    closedList = new List<PathNode>();

    // Initialize the Nodes
    for (int x = 0; x < grid.getWidth(); x++) {
      for(int y = 0; y < grid.getHeight(); y++) {
        PathNode pathNode = grid.getGridObject(x, y);
        pathNode.gCost = int.MaxValue;
        pathNode.calculateFCost();
        pathNode.parent = null;
      }
    }

    // Prime Start Node
    startNode.gCost = 0;
    startNode.hCost = calculateDistanceCost(startNode, endNode);
    startNode.calculateFCost();

    openList.Add(startNode);

    // Algo
    while(openList.Count > 0) {
      PathNode currNode = getLowestFCostNode(openList);
      if (currNode == endNode) {
        // Reached end
        Debug.Log("Path Found");
        return calculatePath(endNode);
      }

      openList.Remove(currNode);
      closedList.Add(currNode);
      int count = 0;
      foreach (PathNode childNode in getChildrenList(currNode)) {
        if (closedList.Contains(childNode)) {
          continue;
        }
        if (!childNode.isTraversable) {
          closedList.Add(childNode);
          continue;
        }

        int tempGCost = currNode.gCost + calculateDistanceCost(currNode, childNode);
        if (tempGCost < childNode.gCost) {
          childNode.parent = currNode;
          childNode.gCost = tempGCost;
          childNode.hCost = calculateDistanceCost(childNode, endNode);
          childNode.calculateFCost();

          if (!openList.Contains(childNode)) {
            openList.Add(childNode);
          }
        }
      }
    }
    Debug.Log("Path Not Found");
    return null;
  }

  private List<PathNode> getChildrenList(PathNode currNode) {
    List<PathNode> childList = new List<PathNode>();

    if (currNode.getX() - 1 >= 0) childList.Add(getNode(currNode.getX() - 1, currNode.getY()));
    if (currNode.getX() + 1 < grid.getWidth()) childList.Add(getNode(currNode.getX() + 1, currNode.getY()));
    if (currNode.getY() - 1 >= 0) childList.Add(getNode(currNode.getX(), currNode.getY() - 1));
    if (currNode.getY() + 1 < grid.getHeight()) childList.Add(getNode(currNode.getX(), currNode.getY() + 1));
    return childList;
  }

  private List<PathNode> calculatePath(PathNode endNode) {
    List<PathNode> path = new List<PathNode>();
    path.Add(endNode);
    PathNode currNode = endNode;
    while (currNode.parent != null) {
      path.Add(currNode.parent);
      currNode = currNode.parent;
    }
    path.Reverse();
    return path;
  }

  private int calculateDistanceCost(PathNode a, PathNode b) {
    int xDistance = Mathf.Abs(a.getX() - b.getX());
    int yDistance = Mathf.Abs(a.getY() - b.getY());
    int remaining = Mathf.Abs(xDistance - yDistance);
    return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
  }

  private PathNode getLowestFCostNode(List<PathNode> pathNodeList) {
    PathNode lowestFCostNode = pathNodeList[0];
    for (int i = 1; i < pathNodeList.Count; i++) {
      if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
        lowestFCostNode = pathNodeList[i];
      }
    }
    return lowestFCostNode;
  }

  private PathNode getNode(int x, int y) {
    return grid.getGridObject(x, y);
  }
}
