using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackEventArgs : EventArgs {

    #region Fields / Properties

    public EnemyType EnemyType { get { return enemyType; } }
    public Vector2 Position { get { return position; } }
    public float RoutineTime { get { return routineTime; } }
    public float SectionTime { get { return sectionTime; } }

    private EnemyType enemyType;
    private Vector2 position;
    private float routineTime;
    private float sectionTime;

    #endregion

    #region Public Behaviour

    public EnemyAttackEventArgs (EnemyType enemyType, Vector2 position, float routineTime) {
        this.enemyType = enemyType;
        this.position = position;
        this.routineTime = routineTime;
        this.sectionTime = routineTime / GameConfig.ShootingRoutineSections;
    }

    #endregion

}