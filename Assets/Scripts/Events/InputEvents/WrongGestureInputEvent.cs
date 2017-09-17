using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WrongGestureInput : EventArgs {

    public GestureInput GestureInput { get { return gestureInput; } }
    private GestureInput gestureInput;

    public WrongGestureInput (GestureInput gestureInput) {
        this.gestureInput = gestureInput;
        Debug.Log("WrongGestureInput");
    }

}