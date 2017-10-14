using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WrongGestureInputEventArgs : EventArgs {

    public GestureInputEventArgs GestureInputEventArgs { get { return gestureInputEventArgs; } }
    private GestureInputEventArgs gestureInputEventArgs;

    public WrongGestureInputEventArgs (GestureInputEventArgs gestureInputEventArgs) {
        this.gestureInputEventArgs = gestureInputEventArgs;
        Debug.Log("WrongGestureInput");
    }

}