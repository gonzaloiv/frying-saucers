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

        public void OnTapInputEvent()   {
            levelController.ToWaveState();
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