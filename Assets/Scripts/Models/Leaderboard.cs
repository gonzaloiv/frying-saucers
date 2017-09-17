using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class Leaderboard {

    public bool HasBeenTutorialPlayed { get { return hasBeenTutorialPlayed; } set { hasBeenTutorialPlayed = value; } }
    private bool hasBeenTutorialPlayed = false;

    public int[] Scores { get { return scores; } set { scores = value; } }
    private int[] scores = new int[5];

    public DateTime[] Dates { get { return dates; } set { dates = value; } }
    private DateTime[] dates = new DateTime[5];

}