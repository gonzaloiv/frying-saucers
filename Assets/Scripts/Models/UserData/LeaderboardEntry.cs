using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class LeaderboardEntry {

    #region Fields / Properties

    public int Score;
    public DateTime Date;

    #endregion

    #region Public Behaviour

    public LeaderboardEntry () {
        this.Score = 0;
        this.Date = DateTime.Now;
    }

    public LeaderboardEntry (int score, DateTime date) {
        this.Score = score;
        this.Date = date;
    }

    #endregion

}