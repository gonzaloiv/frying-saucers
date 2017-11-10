using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class WaveRefillState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            if (currentWave.IsFinished) {
                waveController.ToRoundStartState();
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
            FillWave();
            if (currentWave.WaveRefillGesturesTime != 0) {
                enemyGestureSpawner.ShowGestures(currentWave.ActiveEnemies, currentWave.WaveRefillGesturesTime);
                yield return new WaitForSeconds(currentWave.WaveRefillGesturesTime);
            }
            if (currentWave.WaveRefillPauseTime != 0) {
                waveController.InvokeEnemyAttackStartEvent(currentWave.WaveRefillPauseTime);
                yield return new WaitForSeconds(currentWave.WaveStartPauseTime);
            }
            waveController.ToEnemyAttackState();
        }

        private void FillWave () {
            for (int i = 0; i < currentWave.Enemies.Length; i++) {
                if (!currentWave.Enemies[i].activeInHierarchy && currentWave.RemainingEnemies > 0)
                    currentWave.SetEnemy(waveSpawner.SpawnEnemy(currentWave.WaveData, i), i);
            }
        }

        #endregion

    }

}