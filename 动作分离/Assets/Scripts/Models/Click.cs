using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    IObjectController clickAction;
    public void setClickAction(IObjectController clickAction) {
        this.clickAction = clickAction;
    }
    void OnMouseDown() {
        clickAction.DealClick();
    }
}