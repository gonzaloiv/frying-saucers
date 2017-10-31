using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObject/Wave", order = 1)]
public class WaveData : ScriptableObject {

    public EnemyType[] EnemyTypes { get { return enemyTypes; } }
    public float[] RoutineTime { get { return routineTime; } }

    [Header("Empty for random spawning")]
    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private float[] routineTime = new float[2];
        
}