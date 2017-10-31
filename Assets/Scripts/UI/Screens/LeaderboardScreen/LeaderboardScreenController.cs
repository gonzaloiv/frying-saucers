using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LeaderboardScreenController : MonoBehaviour {

    #region Fields

    private const string NO_DATA = "No data";

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
            if (DataManager.Leaderboard.Scores[i] != 0) {
                scoreLabels[i].text = DataManager.Leaderboard.Scores[i].ToString("00000");
                scoreLabels[i].enabled = true;
                dateLabels[i].text = DataManager.Leaderboard.Dates[i].ToString("yyyy/MM/dd");
                dateLabels[i].enabled = true;
                if ((DateTime.Now - DataManager.Leaderboard.Dates[i]).TotalSeconds <= 5) {
                    scoreLabels[i].GetComponent<BlinkingTextBehaviour>().enabled = true;
                } else {
                    scoreLabels[i].GetComponent<BlinkingTextBehaviour>().enabled = false;
                }
            } else {
                scoreLabels[i].text = NO_DATA;
                scoreLabels[i].enabled = true;
                dateLabels[i].enabled = false;
            }
        }
    }

    #endregion
	
}