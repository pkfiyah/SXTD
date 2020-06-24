using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMarkerController : MonoBehaviour {

    public static Color GHOST_WHITE = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    public Tilemap ghostTileMap;
    public Tilemap entityTileMap;

    private GameObject _ghostPiece;
    private GameMaster _gm;

    public void setGhostPiece(GameObject piece) {
      _ghostPiece = piece;
    }

    public GameObject getGhostPiece() {
      return _ghostPiece;
    }

    public void cleanBoard() {
      ghostTileMap.ClearAllTiles();
    }

    public void cleanState() {
      ghostTileMap.ClearAllTiles();
      Destroy(_ghostPiece);
      _ghostPiece = null;
    }

    void Awake() {
      _gm = this.GetComponent<GameMaster>();
    }

    void FixedUpdate() {
      this.cleanBoard();
      if (_ghostPiece != null) {
        // if (_ghostPiece.GetComponent<SpriteRenderer>() != null) {
        //   _ghostPiece.GetComponent<SpriteRenderer>().color = GhostMarkerController.GHOST_WHITE;
        // }

        Vector3 mouseWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldCoords.z = 0;
        Vector3Int mouseTileCoords = _gm.getTilePositionFromWorldPosition(mouseWorldCoords);
        Tile tileRef = _ghostPiece.GetComponent<Piece>().tile;
        if (tileRef != null) {
          tileRef.color = GhostMarkerController.GHOST_WHITE;
          ghostTileMap.SetTile(mouseTileCoords, tileRef);
        }
        _ghostPiece.transform.position = mouseWorldCoords;
      }
    }
}
