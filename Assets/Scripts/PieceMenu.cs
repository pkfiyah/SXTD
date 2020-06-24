using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMenu : MonoBehaviour {
  public GameMaster _gm;
  public GameObject _wallPrefab;
  private Vector3Int _tileTarget;

  public void repositionMenu() {
    Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mp.z = 0;
    _tileTarget = _gm.getTilePositionFromWorldPosition(mp);
    mp.x += 0.5f;
    this.transform.position = mp;
  }

  public void spawnWall() {
      GameObject currUnit = Instantiate(_wallPrefab, _tileTarget, Quaternion.identity);
      WallTile e = currUnit.GetComponent(typeof(Piece)) as WallTile;
      _gm.setTileGraphic(e.getTilePosition(), e.tile);
      // _gb[mouseTileCoords.x, mouseTileCoords.y].destroySelf();
      // _gb[mouseTileCoords.x, mouseTileCoords.y] = e;
  }

  public void spawnEnemy() {
  }

  public void spawnHearth() {
  }
}
