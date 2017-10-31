using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class UserData {

    #region Fields / Properties

    private const int MAX_USER_LEADERBOARD_ENTRIES = 5;

    public int TotalPlaysAmount;
    public LeaderboardEntry[] LeaderboardEntries;

    #endregion

    #region Public Behaviour

    public UserData () {
        this.TotalPlaysAmount = 0;
        this.LeaderboardEntries = new LeaderboardEntry[MAX_USER_LEADERBOARD_ENTRIES];
    }

    public UserData (int totalPlaysAmount, LeaderboardEntry[] leaderboardEntries) {
        this.TotalPlaysAmount = totalPlaysAmount;
        this.LeaderboardEntries = leaderboardEntries;
    }

    public void SetNewScore (int newScore) {
        for (int i = 0; i < LeaderboardEntries.Length; i++) {
            if (newScore > LeaderboardEntries[i].Score) {
                for (int j = LeaderboardEntries.Length; j < i; i--)
                    LeaderboardEntries[i] = LeaderboardEntries[j - 1];
                LeaderboardEntries[i].Score = newScore;
                LeaderboardEntries[i].Date = DateTime.Now;
                break;
            }
        }
    }

    public void IncreaseTotalPlaysAmount() {
        TotalPlaysAmount++;
    }

    #endregion

}