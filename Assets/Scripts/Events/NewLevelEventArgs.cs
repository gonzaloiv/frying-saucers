using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewLevelEventArgs : EventArgs {
    public NewLevelEventArgs () {
        Debug.Log("NewLevelEvent");
    }
}