using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseButton : MonoBehaviour {
    public float moveAmountX = -2.0f;
    private bool isOpen = true;
    public void HitOpenBloseButton() {
      if (isOpen) {
        this.GetComponent<RectTransform>().parent.position = new Vector3(moveAmountX, 3.5f, 0f);
        isOpen = false;
      } else {
        this.GetComponent<RectTransform>().parent.position = new Vector3(0f, 3.5f, 0f);
        isOpen = true;
      }
    }
}
