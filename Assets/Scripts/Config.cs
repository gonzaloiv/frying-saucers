using UnityEngine;
using System.Collections.Generic;

public class Config {

  #region Board

  public static Vector2 BoardSize = new Vector2(768, 1024); // IPAD DIMENSIONS

  #endregion

}

public enum CollisionLayer {}

public enum EnemyType {
  UFO
}