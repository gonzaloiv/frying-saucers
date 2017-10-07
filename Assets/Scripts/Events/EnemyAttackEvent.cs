using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackEvent : EventArgs {

    public EnemyType EnemyType { get { return enemyType; } }
    private EnemyType enemyType;

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    public float RoutineTime { get { return routineTime; } }
    private float routineTime;

    public float SectionTime { get { return sectionTime; } }
    private float sectionTime;

    public EnemyAttackEvent (EnemyType enemyType, Vector2 position, float routineTime) {
        this.enemyType = enemyType;
        this.position = position;
        this.routineTime = routineTime;
        this.sectionTime = routineTime / GameConfig.ShootingRoutineSections;
        Debug.Log("EnemyAttackEvent " + EnemyType.ToString());
    }

}