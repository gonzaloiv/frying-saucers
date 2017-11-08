using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureInputEventArgs : EventArgs {

    #region Fields / Properties

    public GestureType Type { get { return type; } }
    public float Score { get { return score; } }
    public GestureTime Time { get { return time; } }

    private  GestureType type;
    private float score;
    private GestureTime time;

    #endregion

    #region Public Behaviour

    public GestureInputEventArgs (string gestureClass, float score, GestureTime time) {

        Debug.Log("Gesture Input: " + gestureClass + " " + score);

        if (gestureClass.ToUpper() == GestureType.Circle.ToString().ToUpper()) {
            type = GestureType.Circle;
        } else if (gestureClass.ToUpper() == GestureType.Square.ToString().ToUpper()) {
            type = GestureType.Square;
        } else if (gestureClass.ToUpper() == GestureType.Triangle.ToString().ToUpper()) {
            type = GestureType.Triangle;
        } else if (gestureClass.ToUpper() == GestureType.Cross.ToString().ToUpper()) {
            type = GestureType.Cross;
        } else if (gestureClass.ToUpper() == GestureType.Victory.ToString().ToUpper()) {
            type = GestureType.Victory;
        }

        this.score = score;
        this.time = time;

    }

    #endregion

}