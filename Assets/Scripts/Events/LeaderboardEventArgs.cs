using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeaderboardEventArgs : EventArgs {
    public LeaderboardEventArgs () {
        Debug.Log("LeaderboardEvent");
    }
}