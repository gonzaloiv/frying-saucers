using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class WaveRefillState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            StartCoroutine(EnemyHitRoutine());
        }

        public override void Play () {
            base.Play();
            FillWave();
        }

        #endregion

        #region Private Behaviour

        private void FillWave () {
            GameObject[] currentLevelObjects = waveController.CurrentWaveEnemies;
            for (int i = 0; i < currentLevelObjects.Length; i++) {
                if (!currentLevelObjects[i].activeInHierarchy) {
                    GameObject enemy = waveSpawner.SpawnRandomEnemy(i, player);
                    currentWaveEnemies[i] = enemy;
                    enemyTypeLabelSpawner.SetGestureByIndex(i, enemy.GetComponent<EnemyController>().Enemy);
                    enemyTypeLabelSpawner.ShowGestures(1);
                }
            }
        }

        private IEnumerator EnemyHitRoutine () {
            yield return new WaitForSeconds(1);
            if (currentWaveEnemies.Where(enemy => enemy.activeInHierarchy).Count() == 0)
                waveController.InvokeWaveEndEvent();
            waveController.ToWaveStartState();
        }

        #endregion

    }

}