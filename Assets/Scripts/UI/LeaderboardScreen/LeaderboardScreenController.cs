using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LeaderboardScreenController : MonoBehaviour {

    #region Fields

    private const string NO_DATA = "No data";

    private Text[] scores = new Text[3];
    private Text[] dates = new Text[3];

    #endregion

    #region Mono Behaviour

    void Awake () {
        // TODO: Refactoring this !
        scores[0] = GetComponentsInChildren<Text>()[1];
        scores[1] = GetComponentsInChildren<Text>()[3];
        scores[2] = GetComponentsInChildren<Text>()[5];
        dates[0] = GetComponentsInChildren<Text>()[2];
        dates[1] = GetComponentsInChildren<Text>()[4];
        dates[2] = GetComponentsInChildren<Text>()[6];
    }

    void OnEnable () {
        SetScores();
    }

    #endregion

    #region Private Behaviour

    private void SetScores () {

        for (int i = 0; i < scores.Length; i++) {
            if (DataManager.Leaderboard.Scores[i] != 0) {

                scores[i].text = DataManager.Leaderboard.Scores[i].ToString("00000");
                scores[i].enabled = true;

                dates[i].text = DataManager.Leaderboard.Dates[i].ToString("yyyy/MM/dd");
                dates[i].enabled = true;

                if ((DateTime.Now - DataManager.Leaderboard.Dates[i]).TotalSeconds <= 5) {
                    scores[i].GetComponent<BlinkingTextBehaviour>().enabled = true;
                } else {
                    scores[i].GetComponent<BlinkingTextBehaviour>().enabled = false;
                }

            } else {
                scores[i].text = NO_DATA;
                scores[i].enabled = true;
                dates[i].enabled = false;
            }
        }

    }

    #endregion
	
}