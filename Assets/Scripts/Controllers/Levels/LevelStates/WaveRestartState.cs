using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveRestartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            StartCoroutine(WaveRestartRoutine());
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveRestartRoutine () {
            yield return new WaitForSeconds(0.8f);
            playerController.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            levelController.ToWaveState();
        }

        #endregion

    }

}