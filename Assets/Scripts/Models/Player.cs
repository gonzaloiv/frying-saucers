using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    #region Fields / Properties

    public bool IsDead { get { return lives < 1; } }

    public int Lives { get { return lives; } }
    public int Score { get { return score; } }
    public int Combo { get { return combo; } }

    private int lives;
    private int score;
    private int combo;

    #endregion

    #region Events

    public delegate void PlayerHitEventHandler (PlayerHitEventArgs playerHitEventArgs);
    public static event PlayerHitEventHandler PlayerHitEvent = delegate {};

    #endregion

    #region Public Behaviour

    public void Init (int lives) {
        this.score = 0;
        this.combo = 1;
        this.lives = lives;
    }

    public void DecreaseLives (int livesAmount = 1) {
        this.lives -= livesAmount;
        InvokePlayerHitEvent();
    }

    public void IncreaseScore (GestureTime gestureTime) {
        this.score += (int) Mathf.Ceil(GameConfig.EnemyScore * GetGestureMultiplier(gestureTime) * combo);
    }

    public void ResetCombo () {
        this.combo = 1;
    }

    public void IncreaseCombo (int comboAmount = 1) {
        this.combo += comboAmount;
    }

    #endregion

    #region Private Behaviour

    private void InvokePlayerHitEvent () {
        PlayerHitEvent.Invoke(new PlayerHitEventArgs(this.lives, this.score, this.IsDead));
    }

    private float GetGestureMultiplier (GestureTime gestureTime) {
        switch (gestureTime) {
        case GestureTime.Perfect:
            return 2;
        case GestureTime.TooFast:
            return .5f;
        case GestureTime.TooSlow:
            return .5f;
        default:
            return 1;
        }
    }

    #endregion

}