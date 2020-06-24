using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingOrb : Projectile {

    private GameMaster _gm;
    private float rot = 0.0f;
    private float rad = 3.55f;
    private Vector3 _homeTile;

    public override void Awake() {
      base.Awake();
      _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
      _homeTile = _gm.getWorldPositionFromTilePosition(_gm.getTilePositionFromWorldPosition(transform.position));
      _homeTile.y += 0.4f;
    }


    public override void FixedUpdate() {
      // Idle Animation
      if (_target == null) {
        Vector2 currWorldPos = _rbody.position;
        rot += 10.0f;
        if (rot >= 360.0f) rot = 0.0f;
        float radrot = Mathf.Deg2Rad * rot;
        float targetY = Mathf.Sin(radrot) * rad;
        float targetX = Mathf.Cos(radrot) * rad;
        Vector3 targetWorldPos = new Vector3(_homeTile.x + targetX, _homeTile.y + targetY, 0.0f);
        Vector2 inputVector = new Vector2(targetWorldPos.x - currWorldPos.x, targetWorldPos.y - currWorldPos.y);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        Vector2 newPos = currWorldPos + movement * Time.fixedDeltaTime;
        _rbody.MovePosition(newPos);
      } else {
        base.FixedUpdate();
      }
    }
}
