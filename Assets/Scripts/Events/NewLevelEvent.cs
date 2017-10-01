using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewLevelEvent : EventArgs {
    public NewLevelEvent () {
        Debug.Log("NewLevelEvent");
    }
}