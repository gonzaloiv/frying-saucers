using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Enemy {

  public EnemyType EnemyType { get { return enemyType; } }
  private EnemyType enemyType;

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public Enemy(EnemyType enemyType, Vector2 position) {
    this.enemyType = enemyType;
    this.position = position;
  }
	
}
