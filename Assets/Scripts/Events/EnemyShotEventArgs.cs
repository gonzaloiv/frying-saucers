using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShotEventArgs : EventArgs {

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    public EnemyShotEventArgs (Vector2 position) {
        this.position = position;
    }

}