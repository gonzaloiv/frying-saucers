using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

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
            inputManager.SetActive(true);
            TimeManager.StartTime();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners() {
            
        }

        protected override void RemoveListeners() {

        }

        #endregion

    }

}