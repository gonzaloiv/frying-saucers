using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;
using System;

public struct Wave {

  #region Fields

  public List<Enemy> Enemies { get { return enemies; } }
  private List<Enemy> enemies;

  public int WavePosition { get { return wavePosition; } set { wavePosition = value; } }
  private int wavePosition;

  #endregion

  #region Public Behaviour

  public Wave(int wavePosition) : this() {
    this.wavePosition = wavePosition;
    this.enemies = WaveEnemies();
  }

  public Wave(int wavePosition, List<Enemy> enemies) {
    this.wavePosition = wavePosition;
    this.enemies = enemies;
  }

  #endregion

  #region Private Behaviour

  private List<Enemy> WaveEnemies() {
    List<Enemy> enemies = new List<Enemy>();

    for (int i = 0; i < Config.ENEMY_WAVE_AMOUNT; i++) {
      EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length);
      Vector2 enemyPosition = BoardManager.GetEnemyGridPosition(i, wavePosition);
      EnemyScore enemyScore = (EnemyScore) Enum.Parse(typeof(EnemyScore), enemyType.ToString());

      enemies.Add(new Enemy(enemyType, enemyPosition, enemyScore)); 
    }

    return enemies;
  }

  #endregion
  
}
