using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models {

  public struct UFO {

    public int Score { get { return score; } }
    private int score;

    public UFO(int score = 10) {	
      this.score = score;
    }

  }

}