using UnityEngine;
using System.Collections.Generic;

public class Config {

  public static Vector2 BOARD_SIZE = new Vector2(768, 1024); // IPAD DIMENSIONS
  public const float GRAVITY = 6f;
  public const int TIME_SCALE = 1;

}

public enum CollisionLayer {
  Player = 8,
  Enemy = 9,
  Board = 10
}

public enum EnemyScore {
  UFO = 10
}

public enum EnemyType {
  UFO
}