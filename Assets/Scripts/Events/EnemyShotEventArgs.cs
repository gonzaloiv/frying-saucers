using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShotEventArgs : EventArgs {

    #region Fields / Properties

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    #endregion

    #region Public Behaviour

    public EnemyShotEventArgs (Vector2 position) {
        this.position = position;
    }

    #endregion

}