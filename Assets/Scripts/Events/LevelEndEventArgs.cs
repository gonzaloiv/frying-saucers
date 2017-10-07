using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEndEventArgs : EventArgs {

    public LevelType LevelType { get { return levelType; } }
    private LevelType levelType;

    public LevelEndEventArgs (LevelType levelType) {
        this.levelType = levelType;
    }

}