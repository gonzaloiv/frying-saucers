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
        if (enemyHit) // true when there was a EnemyHitEvent, but the filling is done after the Enemy is desabled from scene.
            FillWave();
    }

    void OnEnable () {
        EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
    }

    void OnDisable () {
        EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
    }

    #endregion

    #region Event Behaviour

    void OnEnemyHitEvent (EnemyHitEvent enemyHitEvent) {
        enemyHit = true;
    }

    #endregion

    #region Private Behaviour

    private void FillWave () {
        List<GameObject> currentLevelObjects = waveController.CurrentWaveEnemyObjects;
        for (int i = 0; i < currentLevelObjects.Count(); i++) {
            if (!currentLevelObjects[i].activeInHierarchy) {
                waveController.AddEnemy(i);
                enemyHit = false;
            }
        }
    }

    #endregion

}
