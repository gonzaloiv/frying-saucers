using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    #region Fields

    public EnemyType EnemyType { get { return enemyType; } }
    public Vector2 Position { get { return position; } }
    public EnemyScore EnemyScore { get { return enemyScore; } }
    public float ShootRoutineTime { get { return shootRoutineTime; } }

    private EnemyType enemyType;
    private Vector2 position;
    private EnemyScore enemyScore;
    private float shootRoutineTime;

    #endregion

    #region Public Behaviour

    public Enemy (EnemyType enemyType, Vector2 position, EnemyScore enemyScore, float shootRoutineTime) {
        this.enemyType = enemyType;
        this.position = position;
        this.enemyScore = enemyScore;
        this.shootRoutineTime = shootRoutineTime;
    }

    public void SetRandomType () {
        this.enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
    }

    #endregion
	
}
