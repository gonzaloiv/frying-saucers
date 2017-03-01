using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;

public class Wave {

  #region Fields

  public List<Enemy> Enemies { get { return enemies; } }
  private List<Enemy> enemies;

  public int WavePosition { get { return wavePosition; } set { wavePosition = value; } }
  private int wavePosition;

  #endregion

  #region Public Behaviour

  public Wave(int wavePosition) {
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
      Vector2 enemyPosition = BoardManager.GetEnemyGridPosition(i, wavePosition);
      enemies.Add(new Enemy((EnemyType) Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length), enemyPosition)); 
    }

    return enemies;
  }

  #endregion
  
}
