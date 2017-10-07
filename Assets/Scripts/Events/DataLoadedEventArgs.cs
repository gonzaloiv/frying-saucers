using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadedEventArgs : EventArgs {

    public Leaderboard Leaderboard { get { return leaderboard; } }
    private Leaderboard leaderboard;

    public DataLoadedEventArgs (Leaderboard leaderboard) {
        this.leaderboard = leaderboard;
    }

}