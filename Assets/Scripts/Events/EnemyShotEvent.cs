using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShotEvent : EventArgs {

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    public EnemyShotEvent (Vector2 position) {
        this.position = position;
    }

}