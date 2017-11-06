using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

    public class PauseState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            pauseScreen.SetActive(true);
            TimeManager.StopTime();
        }

        public override void Exit () {
            base.Exit();
            pauseScreen.SetActive(false);
            TimeManager.StartTime();
        }

        public void OnTapInputEvent()   {
            levelController.ToLevelState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners() {
            InputManager.TapInputEvent += OnTapInputEvent;
        }

        protected override void RemoveListeners() {
            InputManager.TapInputEvent -= OnTapInputEvent;
        }

        #endregion

    }

}