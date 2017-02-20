using UnityEngine;
using System.Collections.Generic;

public class Data {

  #region Levels

  public static Level Level1 = new Level(
    1,
    new List<Wave> (new Wave[] {
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, Vector2.zero),
            new Enemy(EnemyType.UFO, Vector2.one),
            new Enemy(EnemyType.UFO, -Vector2.one)
          }
        )
      ),
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, Vector2.zero),
            new Enemy(EnemyType.UFO, Vector2.one),
            new Enemy(EnemyType.UFO, -Vector2.one)
          }
        )
      ),
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, Vector2.zero),
            new Enemy(EnemyType.UFO, Vector2.one),
            new Enemy(EnemyType.UFO, -Vector2.one)
          }
        )
      )
    })
  );



  #endregion

}