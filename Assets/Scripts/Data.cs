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
            new Enemy(EnemyType.UFO, Vector2.one),
            new Enemy(EnemyType.UFO, Vector2.one * 2),
            new Enemy(EnemyType.UFO, Vector2.one * 3)
          }
        )
      ),
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, -Vector2.one),
            new Enemy(EnemyType.UFO, -Vector2.one * 2),
            new Enemy(EnemyType.UFO, -Vector2.one * 3)
          }
        )
      ),
      new Wave(
        new List<Enemy> ( 
          new Enemy[] {
            new Enemy(EnemyType.UFO, Vector2.one * 4),
            new Enemy(EnemyType.UFO, Vector2.one * 5),
            new Enemy(EnemyType.UFO, Vector2.one * 6)
          }
        )
      )
    })
  );



  #endregion

}