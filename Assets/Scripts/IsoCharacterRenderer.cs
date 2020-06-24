using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    Animator animator;
    int lastDirection;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetDirection(Vector2 direction) {
      string[] directionArray = null;

      if (direction.magnitude < .01f) {
        directionArray = staticDirections;
      } else {
        directionArray = staticDirections; // Should be Run directions
        lastDirection = DirectionToIndex(direction, 8);
      }

      animator.Play(directionArray[lastDirection]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount) {
      Vector2 normDir = dir.normalized;
      float step = 360f / sliceCount;
      float halfstep = step / 2;
      float angle = Vector2.SignedAngle(Vector2.up, normDir);
      angle += halfstep;
      if (angle < 0) {
        angle += 360;
      }
      float stepCount = angle / step;
      return Mathf.FloorToInt(stepCount);
    }
}
