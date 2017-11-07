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
            enemyTypeLabelSpawner.HideGestures();
            StartCoroutine(WaveRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRoutine () {
            enemyTypeLabelSpawner.ShowGestures(GetCurrentWave().Enemies, 2);
            yield return new WaitForSeconds(2);
            waveController.ToEnemyAttackState();
        }

        #endregion

    }

}