using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverEventArgs : EventArgs {

    public int Score { get { return score; } }
    private int score;

    public GameOverEventArgs (int score) {
        this.score = score;
    }

}