using System.Collections.Generic;
using UnityEngine;

public struct Enemy {

    #region Fields

    public EnemyType EnemyType { get { return enemyType; } }
    private EnemyType enemyType;

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    public EnemyScore EnemyScore { get { return enemyScore; } }
    private EnemyScore enemyScore;

    #endregion

    #region Public Behaviour

    public Enemy (EnemyType enemyType, Vector2 position, EnemyScore enemyScore) {
        this.enemyType = enemyType;
        this.position = position;
        this.enemyScore = enemyScore;
    }

    public void SetRandomType () {
        this.enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
    }

    #endregion
	
}
