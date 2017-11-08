using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class WaveStartState : BaseState {

        #region Fields

        private bool playing;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            currentWave.SetEnemies(waveSpawner.SpawnWaveEnemies(currentWave.WaveData));
            currentWave.DecreaseRemainingRounds();
            Debug.Log("CurrentWave.RemainingRounds " + currentWave.RemainingRounds);
            enemyTypeLabelSpawner.HideGestures();
            StartCoroutine(WaveRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRoutine () {
            yield return new WaitForSeconds(currentWave.WaveStartTime);
            enemyTypeLabelSpawner.ShowGestures(currentWave.Enemies, currentWave.WaveStartGesturesTime);
            yield return new WaitForSeconds(currentWave.WaveStartGesturesTime);
            if (currentWave.WaveStartPauseTime != 0)
                yield return new WaitForSeconds(currentWave.WaveStartPauseTime);
            waveController.ToEnemyAttackState();
        }

        #endregion

    }

}