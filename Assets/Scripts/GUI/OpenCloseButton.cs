using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CloseDirection {
  Up,
  Down,
  Left,
  Right
}

public class OpenCloseButton : MonoBehaviour {
    public CloseDirection direction = CloseDirection.Down;

    private Vector2 initial;
    private RectTransform rectParent;
    private bool isOpen = true;

    void Awake() {
      rectParent = this.GetComponent<RectTransform>().parent as RectTransform;
      initial = rectParent.localPosition;
    }

    public void HitOpenBloseButton() {
      Vector2 dirTransform = getDirectionalTransform();
      if (!isOpen) {
        rectParent.localPosition = new Vector3(rectParent.localPosition.x + dirTransform.x, rectParent.localPosition.y + dirTransform.y, 0f);
      } else {
        rectParent.localPosition = new Vector3(initial.x, initial.y, 0f);
      }
    }

    private Vector2 getDirectionalTransform() {

      float newX = 0f;
      float newY = 0f;
      if (isOpen) {
        if (direction == CloseDirection.Left) {
          newX = -rectParent.rect.width * 0.9f;
        } else if (direction == CloseDirection.Right) {
          newX = rectParent.rect.width * 0.9f;
        } else if (direction == CloseDirection.Down) {
          newY = -rectParent.rect.height * 0.9f;
        } else if (direction == CloseDirection.Up) {
          newY = rectParent.rect.height * 0.9f;
        }

        isOpen = false;
      } else {
        isOpen = true;
      }

      return new Vector2(newX, newY);
    }
}
