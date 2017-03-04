using UnityEngine;
using System.Collections.Generic;

public class Config {

  #region Game
  
  public static Vector2 SCREEN_SIZE = new Vector2(768 / 2, 1024 / 2); // IPAD DIMENSIONS
  public const float GRAVITY = 6f;
  public const int TIME_SCALE = 1;
  public const float GESTURE_TIME = 1f;
  public static Vector2 PLAYER_INITIAL_POSITION = new Vector2(0, -1f);
  public const float GESTURE_MIN_SCORE = 0.85f;

  #endregion

  #region Level 

  public const int ENEMY_GRID_ROW_SIZE = 4;
  public const int ENEMY_GRID_COL_SIZE = 4;
  public const int ENEMY_WAVE_AMOUNT = 4;
  public const float ENEMY_MAX_SPEED = 0.1f;

  #endregion

}

public enum CollisionLayer {
  Player = 8,
  Enemy = 9,
  Board = 10
}

public enum SortingLayer {
  Default = 0,
  Background = 1,
  Level = 2,
  UI = 3,
  Top = 4
}

public enum EnemyScore {
  Circle = 10,
  Cross = 10,
  Square = 10,
  Triangle = 10,
  Victory = 10
}

public enum EnemyType {
  Circle,
  Cross,
  Square,
  Triangle,
  Victory
}

public enum GestureType {
  Circle,
  Cross,
  Square,
  Triangle,
  Victory
}