using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

  public static int Lives { get { return lives; } set { lives = value; } }
  private static int lives;

  public static int Score { get { return score; } set { score = value; } }
  private static int score;

  public static int Combo { get { return combo; } set { combo = value; } }
  private static int combo;

  public Player (int lives) {
    Player.score = 0;
    Player.combo = 0;
    Player.lives = lives;
  }

}