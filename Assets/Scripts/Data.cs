using UnityEngine;
using System.Collections.Generic;

public class Data {

  #region Levels

  public static Level Level1 = new Level(
    1,
    new Vector2(0, -4),
    new List<Wave> (new Wave[] {
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, new Vector2(-3, 10)),
            new Enemy(EnemyType.UFO, new Vector2(0, 10)),
            new Enemy(EnemyType.UFO, new Vector2(3, 10))
          }
        )
      )
    })
  );

  #endregion

}