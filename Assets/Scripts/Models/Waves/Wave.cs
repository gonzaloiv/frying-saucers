using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wave {

    #region Fields / Properties

    public WaveData WaveData { get { return waveData; } }
    public GameObject[] Enemies { get { return enemies; } }

    // Wave State
    public bool IsFinished { get { return enemies.Where(enemy => enemy.activeInHierarchy).Count() == 0 && remainingEnemies == 0; } }
    public float RemainingRounds { get { return remainingRounds; } }
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
        ResetWaveEnemies();
        this.enemies = new GameObject[waveData.WaveEnemies.EnemyGridAmount];
        this.enemies = enemies;
    }

    public void DecreaseRemainingRounds () {
        remainingRounds--;
    }

    public void DecreaseRemainingEnemies () {
        remainingEnemies--;
    }

    #endregion

    #region Private Behaviour

    private void ResetWaveEnemies () {
        if (enemies != null) {
            foreach (GameObject enemy in enemies)
                enemy.SetActive(false);
            enemies = null;
        }
    }

    #endregion

}