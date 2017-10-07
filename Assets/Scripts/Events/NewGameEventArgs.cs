using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewGameEventArgs : EventArgs {
    public NewGameEventArgs () {
        Debug.Log("NewGameEvent");
    }
}