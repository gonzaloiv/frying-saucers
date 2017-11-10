using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveStates {

    public class PlayerRespawnState : BaseState {

        #region State Behaviour

        public override void Enter () {
            base.Enter();
            if (!playerController.Player.IsDead)
                StartCoroutine(WaveRestartRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRestartRoutine () {
            yield return new WaitForSeconds(0.8f);
            playerController.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            if (currentWave.WaveStartGesturesTime != 0) {
                enemyGestureSpawner.ShowGestures(currentWave.ActiveEnemies, currentWave.WaveStartGesturesTime);
                yield return new WaitForSeconds(currentWave.WaveStartGesturesTime);
            }
            if (currentWave.WaveStartPauseTime != 0) {
                waveController.InvokeEnemyAttackStartEvent(currentWave.WaveStartPauseTime);
                yield return new WaitForSeconds(currentWave.WaveStartPauseTime);
            }
            waveController.ToEnemyAttackState();
        }

        #endregion

    }

}