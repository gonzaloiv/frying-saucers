using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewGameEvent : EventArgs {
    public NewGameEvent () {
        Debug.Log("NewGameEvent");
    }
}