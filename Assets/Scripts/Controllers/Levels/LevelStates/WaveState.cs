using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveState : BaseState {

        #region Fields

        private bool playing = false;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            StartCoroutine(WaveRoutine());
            gestureManager.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            StopAllCoroutines();
            gestureManager.SetActive(false);
        }

        public override void Play () {
            base.Play();
            if (!playing && waveController.CurrentWaveEnemies.Where(x => x.activeSelf).Count() > 0)
                StartCoroutine(WaveRoutine());
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            StopCoroutine(WaveRoutine());
            levelController.ToRestartState();
        }

        public void OnWaveEndEvent () {
            levelController.ToWaveStartState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
            WaveController.WaveEndEvent += OnWaveEndEvent;
        }

        protected override void RemoveListeners () {
            Player.PlayerHitEvent -= OnPlayerHitEvent;
            WaveController.WaveEndEvent -= OnWaveEndEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRoutine () {
            playing = true;
            float[] waveRoutineTime = GetCurrentWaveData().RoutineTime; 
            float routineTime = Random.Range(waveRoutineTime[0], waveRoutineTime[1]);
            yield return new WaitForSeconds(1);
            GameObject currentEnemy = waveController.GetRandomActiveEnemy();
            currentEnemy.GetComponent<EnemyController>().Play(routineTime);
            yield return new WaitForSeconds(routineTime);
            playing = false;
        }

        #endregion

    }


}