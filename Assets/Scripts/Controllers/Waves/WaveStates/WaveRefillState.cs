using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class WaveRefillState : BaseState {

        #region Fields / Properties

        private Wave currentWave;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            currentWave = GetCurrentWave();
            StartCoroutine(EnemyHitRoutine());
        }

        public override void Play () {
            base.Play();
            FillWave();
        }

        #endregion

        #region Private Behaviour

        private void FillWave () {
            for (int i = 0; i < currentWave.Enemies.Length; i++) {
                if (!currentWave.Enemies[i].activeInHierarchy) {
                    GameObject enemy = waveSpawner.SpawnRandomEnemy(i, player);
                    currentWave.Enemies[i] = enemy;
                    enemyTypeLabelSpawner.ShowGestures(currentWave.Enemies, 2);
                }
            }
        }

        private IEnumerator EnemyHitRoutine () {
            yield return new WaitForSeconds(2);
            if (currentWave.Enemies.Where(enemy => enemy.activeInHierarchy).Count() == 0)
                waveController.InvokeWaveEndEvent();
            waveController.ToWaveStartState();
        }

        #endregion

    }

}