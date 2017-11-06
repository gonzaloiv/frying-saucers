using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveStates {

    public class PlayerRespawnState : BaseState {

        #region State Behaviour

        public override void Enter () {
            base.Enter();
            StartCoroutine(WaveRestartRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRestartRoutine () {
            yield return new WaitForSeconds(0.8f);
            playerController.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            waveController.ToWaveStartState();
        }

        #endregion

    }

}