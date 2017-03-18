using UnityEngine;
using System.Collections.Generic;
using Models;
using System;

public class GameData : MonoBehaviour, IGameData {

  #region Fields

  private const int INITIAL_PLAYER_LIVES = 3;

  public Player Player { get { return player; } }
  private Player player;

  public Level[] Levels { get { return levels; } }
  private Level[] levels;
  
  #endregion

  #region Public Behaviour

  public void InitializePlayer() {
    this.player = new Player(INITIAL_PLAYER_LIVES);
  }

  public void InitializeLevels() {
    levels = new Level[] {
      new Level(new List<Wave> { 
        new Wave(GameData.RandomWaveEnemies(4), new float[] { 2, 2.8f } )
      }) 
    };
  }

  #endregion

  #region Private Behaviour

  private static List<Enemy> RandomWaveEnemies(int enemyAmount) {

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