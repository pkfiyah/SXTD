using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsoController : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsoCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    GameObject playerReticle;
    public GridLayout grid;
    public Tilemap tilemap;
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsoCharacterRenderer>();
        playerReticle = GameObject.Find("Reticle");
        // Tilemap tilemap = this.transform.parent.GetComponent<Tilemap>();
        print("Temp:" + tilemap.WorldToCell(transform.position));
        // transform.position = tilemap.GetCellCenterWorld(cellPosition);
    }

    // Update is called once per frame
    void FixedUpdate() {
      Vector2 currentPos = rbody.position;
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");
      Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
      inputVector = Vector2.ClampMagnitude(inputVector, 1);
      Vector2 movement = inputVector * movementSpeed;
      Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
      isoRenderer.SetDirection(movement);
      rbody.MovePosition(newPos);
      // print("Temp:" + tilemap.WorldToCell(transform.position));
      // print("MouseTemp:" + tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
      // Vector3 thing = grid.WorldToCell(rbody.position);
      // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // Vector3Int cellCoords = grid.WorldToCell(mousePosition);
      // Vector3 midPos = grid.CellToWorld(cellCoords);
      //
      //
      // Vector3Int scrubPos = grid.WorldToCell(midPos);
      // scrubPos.z = 0;
      // print("GridMouseY: " + mousePosition.y);
      //print("GridMouseX: " + grid.WorldToCell(scrubPos));
      Vector3Int cellMousePosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      cellMousePosition.z = 0;
      playerReticle.transform.position = tilemap.GetCellCenterWorld(cellMousePosition);
    }

    void Update()
    {

    }
}
