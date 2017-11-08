using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class WaveEnemies {

    public int EnemyGridAmount { get { return enemyGridAmount; } }
    public int EnemyRefillAmount { get { return enemyRefillAmount; } }
    public EnemyType[] EnemyTypes { get { return enemyTypes; } }

    [SerializeField] private int enemyGridAmount;
    [SerializeField] private int enemyRefillAmount;
    [SerializeField] private EnemyType[] enemyTypes;
    	
}
