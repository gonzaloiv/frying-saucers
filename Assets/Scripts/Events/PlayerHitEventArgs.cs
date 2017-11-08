using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEventArgs {

    #region Fields / Properties

    public int Lives { get { return lives; } }
    public int Score { get { return score; } }
    public bool IsDead { get { return isDead; } }

    private int lives;
    private int score;
    private bool isDead;

    #endregion

    #region Public Behaviour

    public PlayerHitEventArgs (int lives, int score, bool isDead) {
        this.lives = lives;
        this.score = score;
        this.isDead = isDead;
    }

    #endregion

}
