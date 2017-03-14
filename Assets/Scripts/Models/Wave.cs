using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;

public class Wave {

  #region Fields

  public List<Enemy> Enemies { get { return enemies; } }
  private List<Enemy> enemies;

  public int EnemyAmount { get { return enemyAmount; } }
  private int enemyAmount;

  public float[] RoutineTime { get { return routineTime; }  } 
  private float[] routineTime;

  #endregion

  #region Public Behaviour

  public Wave(List<Enemy> enemies, float[] routineTime) {
    this.enemies = enemies;
    this.enemyAmount = enemies.Count();
    this.routineTime = routineTime;
  }

  #endregion
        
}
