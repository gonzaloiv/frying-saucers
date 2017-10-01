using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class RestartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            StartCoroutine(RestartRoutine());
            levelScreenController.gameObject.SetActive(false);
        }

        #endregion

        #region Private Behaviour

        private IEnumerator RestartRoutine () {
            yield return new WaitForSeconds(0.8f);
            player.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            levelController.ToPlayState();
        }

        #endregion

    }

}