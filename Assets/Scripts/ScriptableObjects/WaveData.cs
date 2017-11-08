using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObject/Wave", order = 1)]
public class WaveData : ScriptableObject {

    // Wave Data
    public int[] Rounds { get { return rounds; } }
    public WaveEnemies WaveEnemies { get { return waveEnemies; } }

    // Wave Time
    public float WaveStartGesturesTime { get { return waveStartGesturesTime; } }
    public float WaveStartTime { get { return waveStartTime; } }
    public float WaveStartPauseTime { get { return waveStartPauseTime; } }
    public float WaveRefillGesturesTime { get { return waveRefillGesturesTime; } }
    public float WaveRefillPauseTime { get { return waveRefillPauseTime; } }
    public float[] EnemyRoutineTime { get { return enemyRoutineTime; } }

    // Wave Data
    [SerializeField] private int[] rounds = new int[2];
    [SerializeField] private WaveEnemies waveEnemies;

    // Wave Time
    [SerializeField] private float waveStartTime;
    [SerializeField] private float waveStartGesturesTime;
    [SerializeField] private float waveStartPauseTime;
    [SerializeField] private float waveRefillGesturesTime;
    [SerializeField] private float waveRefillPauseTime;
    [SerializeField] private float[] enemyRoutineTime = new float[2];
        
}