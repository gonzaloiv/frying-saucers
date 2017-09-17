using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEndEvent : EventArgs {
    public LevelEndEvent () {
        Debug.Log("LevelEndEvent");
    }
}