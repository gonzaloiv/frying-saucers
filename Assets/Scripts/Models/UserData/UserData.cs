using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable] public class UserData {

    #region Fields / Properties

    private const int MAX_USER_LEADERBOARD_ENTRIES = 5;

    public int TotalPlaysAmount;
    public List<LeaderboardEntry> LeaderboardEntries;

    #endregion

    #region Public Behaviour

    public UserData () {
        this.TotalPlaysAmount = 0;
        this.LeaderboardEntries = new List<LeaderboardEntry>();
    }

    public UserData (int totalPlaysAmount, List<LeaderboardEntry> leaderboardEntries) {
        this.TotalPlaysAmount = totalPlaysAmount;
        this.LeaderboardEntries = leaderboardEntries;
    }

    public void SetTotalPlaysAmount (int amount) {
        this.TotalPlaysAmount = amount;
    }

    public void SetLeaderboardEntries (List<LeaderboardEntry> leaderboardEntries) {
        this.LeaderboardEntries = leaderboardEntries;
    }

    public void IncreaseTotalPlaysAmount () {
        TotalPlaysAmount++;
    }

    public void AddNewScore (LeaderboardEntry leaderboardEntry) {
        LeaderboardEntries.Add(leaderboardEntry);
        LeaderboardEntries = LeaderboardEntries.OrderByDescending(entry => entry.Score).ThenByDescending(entry => entry.Date).ToList();
        if (LeaderboardEntries.Count > MAX_USER_LEADERBOARD_ENTRIES)
            LeaderboardEntries.RemoveAt(LeaderboardEntries.Count - 1);
    }

    #endregion

}