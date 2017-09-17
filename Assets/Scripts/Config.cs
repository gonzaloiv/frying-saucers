using UnityEngine;
using System.Collections.Generic;

public class Config {

    #region Game

    public const int INITIAL_TIME_SCALE = 1;
    public const float GESTURE_STROKE_TIME = 0.5f;
    public const float GESTURE_MIN_SCORE = 0.85f;
    public const int RANDOM_WAVE_ENEMY_AMOUNT = 4;

    #endregion

    #region Level

    public static Vector2 PLAYER_INITIAL_POSITION = new Vector2(0, -1f);
    public const float ENEMY_MAX_SPEED = 10f;
    public const int ENEMY_SCORE = 10;
    public const float ENEMY_INITIAL_Y_POSITION = 3.5f;
    public const int SHOOTING_ROUTINE_PARTS = 6;

    #endregion

}