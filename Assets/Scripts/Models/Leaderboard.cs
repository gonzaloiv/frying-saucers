﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Models {

  [Serializable]
  public class Leaderboard {
  
    public int[] Scores { get { return scores; } set { scores = value; } }
    private int[] scores = new int[5];

    public DateTime[] Dates { get { return dates; } set { dates = value; } }
    private DateTime[] dates = new DateTime[5];

  }

}