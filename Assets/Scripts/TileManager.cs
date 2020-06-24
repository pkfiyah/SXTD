using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    // private GameObject _camera;
    public Tilemap _tilemap;
    public Tilemap _groundTilemap;
    public Transform _tm;

    private float _height;
    private int _row;
    private int _spawnNum;
    private float _spawnTimer;

    public Tile _groundTile;
    public Tile _obsticleTile;
    public float _spawnTimerMax = 3.0f;
    public float _maxHeight = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
      // _camera = GameObject.Find("Main Camera");
      _row = 0;
      _height = _maxHeight;
      _spawnNum = 4;
      _spawnTimer = 0.0f;
      _tm.position = new Vector3(0, _height, 0);
      // _camera = GameObject.Find("Main Camera");
      spawnTiles(_tilemap, 4);
    }

    // Update is called once per frame
    void Update()
    {
      _spawnTimer += Time.deltaTime;
      if (_tm.localPosition.y > 0.0f) {
        // print("tock:" + _tm.localPosition.y );
        _tm.position = new Vector3(0, easeQuad(_spawnTimer, _maxHeight, 0 - _maxHeight , _spawnTimerMax), 0);
        //_camera.transform.position = new Vector3(easeLin(_spawnTimer, _camera.transform.localPosition.x,_groundTilemap.CellToWorld(new Vector3Int(_row, 0, 0)).x - _camera.transform.localPosition.x, _spawnTimerMax), easeLin(_spawnTimer, _camera.transform.localPosition.y, _groundTilemap.CellToWorld(new Vector3Int(_row, 0, 0)).y - _camera.transform.localPosition.y, _spawnTimerMax), -10);
        print(_groundTilemap.CellToWorld(new Vector3Int(_row, 0, 0)));
      }

      if (_spawnTimer >= _spawnTimerMax) {
        _spawnTimer = 0.0f;
        _tm.position = new Vector3(0, _maxHeight, 0);
        spawnTiles(_groundTilemap, _spawnNum);
        _spawnNum = Random.Range(0, 4) + 1;
        destroyTiles(_tilemap);
        _height = _maxHeight;
        _row++;
        spawnTiles(_tilemap, _spawnNum);
      }
    }

    void spawnTiles(Tilemap tilemap, int tilesToSpawn) {
      for (int i = 0; i > -tilesToSpawn; i--) {
        tilemap.SetTile(new Vector3Int(_row, i, 0), _groundTile);
      }
    }

    void destroyTiles(Tilemap tilemap) {
      tilemap.ClearAllTiles();
    }

    float easeLin (float elaspedTime, float startPosition, float change, float totalAnimationTime) {
      return  change * (elaspedTime/totalAnimationTime) + startPosition;
    }

    float easeQuad (float elaspedTime, float startPosition, float change, float totalAnimationTime) {
      return change * Mathf.Pow(elaspedTime/totalAnimationTime, 3) + startPosition;
    }
}
