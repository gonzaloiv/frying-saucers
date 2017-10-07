using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightGestureInputEventArgs : EventArgs {

    public GestureInputEventArgs GestureInputEventArgs { get { return gestureInputEventArgs; } }
    private GestureInputEventArgs gestureInputEventArgs;

    public RightGestureInputEventArgs (GestureInputEventArgs gestureInputEventArgs) {
        this.gestureInputEventArgs = gestureInputEventArgs;
        Debug.Log("RightGestureInput " + gestureInputEventArgs.Time.ToString());
    }

}