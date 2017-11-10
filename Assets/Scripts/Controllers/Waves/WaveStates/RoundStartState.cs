using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class RoundStartState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            currentWave.SetEnemies(waveSpawner.SpawnWaveEnemies(currentWave.WaveData));
            currentWave.DecreaseRemainingRounds();
            enemyGestureSpawner.HideGestures();
            StartCoroutine(WaveRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRoutine () {
            yield return new WaitForSeconds(currentWave.WaveStartTime);
            enemyGestureSpawner.ShowGestures(currentWave.ActiveEnemies, currentWave.WaveStartGesturesTime);
            yield return new WaitForSeconds(currentWave.WaveStartGesturesTime);
            if (currentWave.WaveStartPauseTime != 0) {
                waveController.InvokeEnemyAttackStartEvent(currentWave.WaveStartPauseTime);
                yield return new WaitForSeconds(currentWave.WaveStartPauseTime);
            }
            waveController.ToEnemyAttackState();
        }

        #endregion

    }

}