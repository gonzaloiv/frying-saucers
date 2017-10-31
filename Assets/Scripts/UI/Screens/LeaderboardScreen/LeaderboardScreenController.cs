using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LeaderboardScreenController : MonoBehaviour {

    #region Fields

    private const string NO_DATA_SCORE_LABEL = "No data";
    private const string NO_DATA_DATE_LABEL = "6666/66/66";

    [SerializeField] private Text[] scoreLabels = new Text[3];
    [SerializeField] private Text[] dateLabels = new Text[3];

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        SetScores();
    }

    #endregion

    #region Private Behaviour

    private void SetScores () {
        for (int i = 0; i < scoreLabels.Length; i++) {
            LeaderboardEntry leaderboardEntry = DataManager.UserData.LeaderboardEntries[i];
            if (leaderboardEntry != null && leaderboardEntry.Score != 0) {
                scoreLabels[i].text = leaderboardEntry.Score.ToString("00000");
                dateLabels[i].text = leaderboardEntry.Date.ToString("yyyy/MM/dd");
                scoreLabels[i].GetComponent<BlinkingTextBehaviour>().enabled = (DateTime.Now - leaderboardEntry.Date).TotalSeconds <= 5 ? true : false;
            } else {
                scoreLabels[i].text = NO_DATA_SCORE_LABEL;
                dateLabels[i].text = NO_DATA_DATE_LABEL;
            }
        }
    }

    #endregion
	
}