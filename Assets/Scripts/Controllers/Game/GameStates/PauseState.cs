using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class PauseState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            pauseScreen.SetActive(true);
            inputManager.SetActive(false);
            TimeManager.StopTime();
        }

        public override void Exit () {
            base.Exit();
            pauseScreen.SetActive(false);
            TimeManager.StartTime();
        }

        #endregion

    }

}