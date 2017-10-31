using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class Leaderboard {

    public bool IsFirstPlay { get { return isFirstPlay; } set { isFirstPlay = value; } }
    private bool isFirstPlay = true;

    public int[] Scores { get { return scores; } set { scores = value; } }
    private int[] scores = new int[5];

    public DateTime[] Dates { get { return dates; } set { dates = value; } }
    private DateTime[] dates = new DateTime[5];

}