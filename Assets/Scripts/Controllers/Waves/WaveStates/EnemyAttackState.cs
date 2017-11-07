using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class EnemyAttackState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            gestureManager.SetActive(true); // Needs to start listening before EnemyAttackRoutine()
            StartCoroutine(EnemyAttackRoutine());
        }

        public override void Exit () {
            base.Exit();
            gestureManager.SetActive(false);
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            if (!playerHitEventArgs.IsDead)
                waveController.ToPlayerRespawnState();
        }

        public void OnEnemyHitEvent () {
            waveController.ToWaveRefillState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
            EnemyController.EnemyHitEvent += OnEnemyHitEvent;
        }

        protected override void RemoveListeners () {
            Player.PlayerHitEvent -= OnPlayerHitEvent;
            EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator EnemyAttackRoutine () {
            Wave currentWave = GetCurrentWave();
            float[] waveRoutineTime = currentWave.RoutineTime; 
            GameObject[] currentWaveEnemies = currentWave.Enemies;
            EnemyController enemyController = currentWaveEnemies[Random.Range(0, currentWaveEnemies.Length)].GetComponent<EnemyController>();
            Debug.Log("EnemyAttackRoutine " + enemyController.gameObject.activeInHierarchy);
            enemyController.ToAttackState();
            yield return new WaitForSeconds(enemyController.Enemy.ShootRoutineTime);
        }

        #endregion

    }

}