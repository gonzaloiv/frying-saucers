using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveState : BaseState {

        #region Fields

        private WaveData currentWaveData;
        private bool playing = false;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            currentWaveData = GetCurrentWaveData();
            StartCoroutine(WaveRoutine());
            waveRefillBehaviour.enabled = GetCurrentLevelData().LevelType != LevelType.TutorialLevel;
        }

        public override void Exit () {
            base.Exit();
            StopAllCoroutines();
        }

        public override void Play () {
            base.Play();
            if (!playing && waveController.CurrentWaveEnemies.Where(x => x.activeSelf).Count() > 0)
                StartCoroutine(WaveRoutine());
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            StopCoroutine(WaveRoutine());
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
        }

        protected override void RemoveListeners () {
            Player.PlayerHitEvent -= OnPlayerHitEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRoutine () {
            playing = true;
            float routineTime = Random.Range(currentWaveData.RoutineTime[0], currentWaveData.RoutineTime[1]);
            yield return new WaitForSeconds(1);
            GameObject currentEnemy = waveController.GetRandomActiveEnemy();
            currentEnemy.GetComponent<EnemyBehaviour>().Play(routineTime);
            yield return new WaitForSeconds(routineTime);
            playing = false;
        }

        #endregion

    }


}