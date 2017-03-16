using UnityEngine;
using System.Collections.Generic;
using Models;
using System;

public class TutorialData : MonoBehaviour, IGameData {

  #region Fields

  public Player Player { get { return player; } }
  private Player player;

  public Level[] Levels { get { return levels; } }
  private Level[] levels;
  
  #endregion

  #region Public Behaviour

  public void InitializePlayer() {
    player = new Player(999);
  }

  public void InitializeLevels() {
    this.levels = new Level[] {
      new Level(new List<Wave> { 
        new Wave(new List<Enemy> { TutorialData.EnemyByType(EnemyType.Circle) }, new float[] { 2, 2.8f } ) 
//        new Wave(new List<Enemy> { TutorialData.EnemyByType(EnemyType.Triangle) }, new float[] { 2, 2.8f } ),
//        new Wave(new List<Enemy> { TutorialData.EnemyByType(EnemyType.Cross) }, new float[] { 2, 2.8f } )
      })
    };
  }

  #endregion

  #region Private Behaviour

  private static Enemy EnemyByType(EnemyType enemyType) {
    Vector2[] grid = Board.EnemyGrid(1);
    Vector2 enemyPosition = grid[0] - new Vector2(0, 1f); // TODO: doing this properly.., the wave should have an y position
    EnemyScore enemyScore = (EnemyScore) Enum.Parse(typeof(EnemyScore), enemyType.ToString());
    return new Enemy(enemyType, enemyPosition, enemyScore);
  }

  private static Enemy RandomEnemy() {
    return EnemyByType((EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length));
  }

  #endregion

}