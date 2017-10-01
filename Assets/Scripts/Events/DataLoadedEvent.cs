using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadedEvent : EventArgs {

    public Leaderboard Leaderboard { get { return leaderboard; } }
    private Leaderboard leaderboard;

    public DataLoadedEvent (Leaderboard leaderboard) {
        this.leaderboard = leaderboard;
    }

}