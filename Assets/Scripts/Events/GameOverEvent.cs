using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverEvent : EventArgs {

    public int Score { get { return score; } }
    private int score;

    public GameOverEvent (int score) {
        this.score = score;
        Debug.Log("GameOverEvent " + this.score);
    }

}