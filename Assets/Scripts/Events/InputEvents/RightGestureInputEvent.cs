using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightGestureInput : EventArgs {

    public GestureInput GestureInput { get { return gestureInput; } }
    private GestureInput gestureInput;

    public RightGestureInput (GestureInput gestureInput) {
        this.gestureInput = gestureInput;
        Debug.Log("RightGestureInput " + gestureInput.Time.ToString());
    }

}