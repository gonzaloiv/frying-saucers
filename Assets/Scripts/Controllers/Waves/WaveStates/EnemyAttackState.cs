using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WaveStates {

    public class EnemyAttackState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            StartCoroutine(EnemyAttackRoutine());
        }

        public override void Exit () {
            base.Exit();
            StopAllCoroutines();
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            waveController.ToPlayerRespawnState();
        }

        public void OnEnemyHitEvent () {
            StartCoroutine(EnemyRefillRoutine());
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
            float[] waveRoutineTime = currentWave.RoutineTime; 
            EnemyController enemyController = currentWave.RandomActiveEnemy.GetComponent<EnemyController>();
            enemyController.ToAttackState();
            yield return new WaitForSeconds(enemyController.Enemy.ShootRoutineTime);
        }

        private IEnumerator EnemyRefillRoutine () {
            Debug.Log("EnemyRefillRoutine " + Time.time);
            yield return new WaitForSeconds(1f); // Waiting for Enemy "Disable" animation...
            waveController.ToWaveRefillState(); // TODO: Getting to the next wave or restarting the current wave if the rounds are not finished
        }

        #endregion

    }

}