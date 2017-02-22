using UnityEngine;
using System.Collections.Generic;

public class Config {

  #region Board

  public static Vector2 BOARD_SIZE = new Vector2(768, 1024); // IPAD DIMENSIONS

  #endregion

  #region Physics

  public const float GRAVITY = 9f;

  #endregion

}

public enum CollisionLayer {
  Player = 8,
  Enemy = 9,
  Board = 10
}

public enum EnemyType {
  UFO
}