using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator {
    // Whats a level Have?
    private const int LEFT_UNSTABLE_GROUND_MARGIN = 2;
    private Vector2Int dimensions;
    private Rect unstableGroundArea;
    private Piece[,] level;

    public LevelGenerator() {
      dimensions = new Vector2Int(12, 12);
      InitializeBoard();
    }

    public LevelGenerator(int x, int y) {
      dimensions = new Vector2Int(x, y);
      InitializeBoard();
    }

    public Piece[,] GetLevel() {
      return level;
    }

    public Rect GetUnstableGround() {
      return unstableGroundArea;
    }

    private void InitializeBoard() {
      level = new Piece[dimensions.x, dimensions.y];
      // Empty area to start
      for(int i = 0; i < dimensions.x; i++) {
        for (int j = 0; j < dimensions.y; j++) {
          level[i, j] = new Piece(PieceType.Empty);
        }
      }

      // Add features in all levels here
      DefineEnemySpawnArea();
      DefineHearth();
      DefinePrismiteNodes();
    }

    // Not along sides, just under half of stage
    private void DefineEnemySpawnArea() {
      int yStart = (int)(dimensions.y / 2) + 1;
      unstableGroundArea = new Rect(LEFT_UNSTABLE_GROUND_MARGIN, yStart, dimensions.x - LEFT_UNSTABLE_GROUND_MARGIN * 2, dimensions.y - yStart);
      for (int i = (int)unstableGroundArea.x; i < (int)(unstableGroundArea.x + unstableGroundArea.width); i++) {
        for (int j = (int)unstableGroundArea.y; j < (int)(unstableGroundArea.y + unstableGroundArea.height); j++) {
          level[i, j] = new Piece(PieceType.UnstableGround);
        }
      }
    }


    private void DefinePrismiteNodes() {
      // minimum distance from spawns, force one close to hearth
      int nodeCount = 0;
      while (nodeCount < 3) {
        int randX = Random.Range(1, dimensions.x - 1);
        int randY = Random.Range(1, dimensions.y - 1);
        if (ScanArea(randX, randY, PieceType.UnstableGround) && ScanArea(randX, randY, PieceType.Hearth) && ScanArea(randX, randY, PieceType.Prismite)) {
          level[randX, randY] = new Piece(PieceType.Prismite);
          nodeCount++;
        }
      }
    }

    private void DefineHearth() {
        // randomly first two rows for now
        int randX = Random.Range(0, dimensions.x);
        int randY = Random.Range(0, 2);
        level[randX, randY] = new Piece(PieceType.Hearth);
    }

    private bool ScanArea(int x, int y, PieceType avoidType) {
      if (x < 0 || x >= dimensions.x || y < 0 || y >= dimensions.y) return false;
      if (level[x, y].type == avoidType) return false;
      if ((y - 1) < 0 || level[x, y - 1].type == avoidType) return false;
      if ((y + 1) >= dimensions.y || level[x, y + 1].type == avoidType) return false;
      if ((x - 1) < 0 || level[x - 1, y].type == avoidType) return false;
      if ((x + 1) >= dimensions.x || level[x - 1, y - 1].type == avoidType) return false;
      if (level[x - 1, y + 1].type == avoidType) return false;
      if (level[x + 1, y].type == avoidType) return false;
      if (level[x + 1, y - 1].type == avoidType) return false;
      if (level[x + 1, y + 1].type == avoidType) return false;
      return true;
    }

}
