using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObject/Wave", order = 1)]
public class WaveData : ScriptableObject {

    public EnemyType[] EnemiesType { get { return enemiesType; } }
    [SerializeField] private EnemyType[] enemiesType;

    public float[] RoutineTime { get { return routineTime; } }
    [SerializeField] private float[] routineTime = new float[2];
        
}