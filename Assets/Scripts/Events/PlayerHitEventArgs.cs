using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEventArgs {

    #region Fields / Properties

    public int Lives { get { return lives; } }
    private int lives;

    public int Score { get { return score; } }
    private int score;

    public bool IsDead { get { return isDead; } }
    private bool isDead;

    #endregion

    #region Public Behaviour

    public PlayerHitEventArgs (int lives, int score, bool isDead) {
        this.lives = lives;
        this.score = score;
        this.isDead = isDead;
        Debug.Log("PlayerHitEventArgs " + this.lives);
    }

    #endregion

}
