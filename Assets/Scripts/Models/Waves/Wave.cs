using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wave {

    #region Fields / Properties

    // Wave State

    public WaveData WaveData { get { return waveData; } }
    public GameObject[] Enemies { get { return enemies; } }
    public GameObject[] ActiveEnemies { get { return enemies.Where(enemy => enemy.activeInHierarchy).ToArray(); } }
    public GameObject RandomActiveEnemy { get { return ActiveEnemies[Random.Range(0, ActiveEnemies.Length)]; } }
    public float RemainingRounds { get { return remainingRounds; } }
    public bool IsFinished { get { return ActiveEnemies.Count() == 0 && remainingRounds == 0; } }
    public int RemainingEnemies { get { return remainingEnemies; } }

    // Wave Times
    public float WaveStartTime { get { return waveData.WaveStartTime; } }
    public float WaveStartGesturesTime { get { return waveData.WaveStartGesturesTime; } }
    public float WaveStartPauseTime { get { return waveData.WaveStartPauseTime; } }
    public float WaveRefillGesturesTime { get { return waveData.WaveRefillGesturesTime; } }
    public float WaveRefillPauseTime { get { return waveData.WaveRefillPauseTime; } }
    public float[] RoutineTime { get { return waveData.EnemyRoutineTime; } }

    private WaveData waveData;
    private GameObject[] enemies;

    private float remainingRounds;
    private int remainingEnemies;

    #endregion

    #region Public Behaviour

    public void Init (WaveData waveData) {
        SetWaveData(waveData);
        remainingRounds = Random.Range(waveData.Rounds[0], waveData.Rounds[1]);
        remainingEnemies = waveData.WaveEnemies.EnemyRefillAmount;
    }

    public void SetWaveData (WaveData waveData) {
        this.waveData = waveData;
    }

    public void SetEnemies (GameObject[] enemies) {
        this.enemies = new GameObject[waveData.WaveEnemies.EnemyGridAmount];
        this.enemies = enemies;
    }

    public void SetEnemy (GameObject enemy, int index) {
        this.enemies[index] = enemy;
        DecreaseRemainingEnemies();
    }

    public void DecreaseRemainingRounds () {
        remainingRounds--;
    }

    public void ResetWaveEnemies () {
        if (enemies == null)
            return;
        foreach (GameObject enemy in enemies)
            enemy.SetActive(false);
    }

    #endregion

    #region Private Behaviour

    private void DecreaseRemainingEnemies () {
        remainingEnemies--;
    }

    #endregion

}