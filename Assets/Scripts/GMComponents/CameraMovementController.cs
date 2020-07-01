using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    private Camera _cam;

    void Awake() {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate() {
      Vector3 currWorldPos = _cam.transform.position;
      if (Input.GetAxis("Horizontal") != 0.0f ||  Input.GetAxis("Vertical") != 0.0f) {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * 1.0f;
        Vector3 newPos = currWorldPos + new Vector3(movement.x, movement.y, -10) * Time.fixedDeltaTime;
        transform.position = newPos;
      }
    }
}
