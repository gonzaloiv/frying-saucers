using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEndEvent : EventArgs {

    public LevelType LevelType { get { return levelType; } }
    private LevelType levelType;

    public LevelEndEvent (LevelType levelType) {
        this.levelType = levelType;
    }

}