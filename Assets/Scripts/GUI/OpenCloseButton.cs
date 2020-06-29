using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseButton : MonoBehaviour {
    private bool isOpen = true;
    public void HitOpenBloseButton() {
      RectTransform parent = this.GetComponent<RectTransform>().parent as RectTransform;
      if (isOpen) {
        parent.localPosition = new Vector3(-parent.rect.width * 0.9f, parent.localPosition.y, 0f);
        isOpen = false;
      } else {
        parent.localPosition = new Vector3(0f, parent.localPosition.y, 0f);
        isOpen = true;
      }
    }
}
