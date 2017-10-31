using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    #region Fields / Properties

    public int Lives { get { return lives; } }
    public int Score { get { return score; } }
    public int Combo { get { return combo; } }

    public bool IsDead { get { return lives < 1; } }

    private int lives;
    private int score;
    private int combo;

    #endregion

    #region Public Behaviour

    public Player (int lives) {
        this.score = 0;
        this.combo = 0;
        this.lives = lives;
    }

    public void DecreaseLives(int livesAmount = 1) {
        this.lives -= livesAmount;
    }

    public void IncreaseScore(int scoreAmount) {
        this.score += scoreAmount;
    }

    public void ResetCombo() {
        this.combo = 1;
    }

    public void IncreaseCombo(int comboAmount = 1) {
        this.combo += comboAmount;
    }

    #endregion

}