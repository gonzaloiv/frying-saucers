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
        Init(enemyType, position, enemyScore, shootRoutineTime);
    }

    public Enemy (EnemyType enemyType, Vector2 position, float[] waveRoutineTime) {
        EnemyScore enemyScore = (EnemyScore) (int) enemyType;
        float enemyShootRoutineTime = Random.Range(waveRoutineTime[0], waveRoutineTime[1]);
        Init(enemyType, position, enemyScore, enemyShootRoutineTime);
    }

    // Random EnemyType Constructor
    public Enemy (Vector2 position, float[] waveRoutineTime) {
        EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
        float enemyShootRoutineTime = Random.Range(waveRoutineTime[0], waveRoutineTime[1]);
        Init(enemyType, position, enemyScore, enemyShootRoutineTime);
    }

    public void SetRandomType () {
        this.enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
    }

    #endregion

    #region Private Behaviour

    private void Init (EnemyType enemyType, Vector2 position, EnemyScore enemyScore, float shootRoutineTime) {
        this.enemyType = enemyType;
        this.position = position;
        this.enemyScore = enemyScore;
        this.shootRoutineTime = shootRoutineTime;
    }

    #endregion
	
}
