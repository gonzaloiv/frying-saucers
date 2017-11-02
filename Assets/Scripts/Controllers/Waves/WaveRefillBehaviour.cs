using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveRefillBehaviour : MonoBehaviour {

    #region Fields

    private WaveController waveController;
    private bool enemyHit = false;

    #endregion

    #region Mono Behaviour

    void Awake () {
        waveController = GetComponent<WaveController>();
    }

    void Update () {
        if (enemyHit) // true when there was a EnemyHitEvent, but the filling is done after the Enemy is disabled from scene.
            FillWave();
    }

    void OnEnable () {
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
    }

    void OnDisable () {
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyHitEvent () {
        enemyHit = true;
    }

    #endregion

    #region Private Behaviour

    private void FillWave () {
        GameObject[] currentLevelObjects = waveController.CurrentWaveEnemies;
        for (int i = 0; i < currentLevelObjects.Length; i++) {
            if (!currentLevelObjects[i].activeInHierarchy) {
                waveController.AddEnemy(i);
                enemyHit = false;
            }
        }
    }

    #endregion

}
