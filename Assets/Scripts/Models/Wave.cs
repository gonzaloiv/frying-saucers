using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

    #region Fields / Properties

    public GameObject[] Enemies { get { return enemies; } }
    public float[] RoutineTime { get { return waveData.RoutineTime; } }

    private WaveData waveData;
    private GameObject[] enemies;

    #endregion

    #region Public Behaviour

    public void Init (WaveData waveData, GameObject[] enemies) {
        ResetWaveEnemies();
        SetWaveData(waveData);
        SetWaveEnemies(enemies);
    }

    public void SetWaveData (WaveData waveData) {
        this.waveData = waveData;
    }

    public void SetWaveEnemies (GameObject[] enemies) {
        this.enemies = enemies;
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