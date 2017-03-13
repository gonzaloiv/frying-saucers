using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;
using System;

public struct Wave {

  #region Fields

  public List<Enemy> Enemies { get { return enemies; } }
  private List<Enemy> enemies;

  #endregion

  #region Public Behaviour

  public Wave(int enemyAmount) : this() {
    this.enemies = WaveEnemies(enemyAmount);
  }

  public Wave(List<Enemy> enemies) {
    this.enemies = enemies;
  }

  #endregion

  #region Private Behaviour

  private List<Enemy> WaveEnemies(int enemyAmount) {

    List<Enemy> enemies = new List<Enemy>();
    Vector2[] grid = Board.EnemyGrid(enemyAmount);

    for (int i = 0; i < grid.Length; i++) {
      EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length);
      Vector2 enemyPosition = grid[i];
      EnemyScore enemyScore = (EnemyScore) Enum.Parse(typeof(EnemyScore), enemyType.ToString());
      enemies.Add(new Enemy(enemyType, enemyPosition, enemyScore)); 
    }

    return enemies;

  }

  #endregion
  
}
