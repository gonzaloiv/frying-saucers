using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class WaveRefillState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            currentWave.SetEnemies(currentWave.Enemies.Where(enemy => enemy.activeInHierarchy).ToArray());
            if (currentWave.IsFinished) {
                waveController.ToWaveStartState();
            } else {
                StartCoroutine(WaveRefillRoutine());
            }
        }

        public override void Exit () {
            base.Exit();
            StopAllCoroutines();
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRefillRoutine () {
            if (currentWave.RemainingEnemies != 0) // The routine is called even if there're no enemies to spawn
                FillWave();
            if (currentWave.WaveRefillGesturesTime != 0)
                enemyTypeLabelSpawner.ShowGestures(currentWave.Enemies, currentWave.WaveRefillGesturesTime);
            yield return new WaitForSeconds(currentWave.WaveRefillGesturesTime);
            yield return new WaitForSeconds(currentWave.WaveStartPauseTime);
            waveController.ToEnemyAttackState();
        }

        private void FillWave () {
            currentWave.DecreaseRemainingEnemies();
            for (int i = 0; i < currentWave.Enemies.Length; i++) {
                if (!currentWave.Enemies[i].activeInHierarchy) {
                    GameObject enemy = waveSpawner.SpawnEnemy(currentWave.WaveData, i);
                    currentWave.Enemies[i] = enemy;
                }
            }
        }

        #endregion

    }

}