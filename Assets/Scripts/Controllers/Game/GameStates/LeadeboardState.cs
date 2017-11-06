using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LeaderboardState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            leaderboardScreen.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            leaderboardScreen.SetActive(false);
        }

        public void OnTapInputEvent() {
            gameController.ToMainMenuState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            InputManager.TapInputEvent += OnTapInputEvent;
        }

        protected override void RemoveListeners () {
            InputManager.TapInputEvent -= OnTapInputEvent;
        }

        #endregion

    }

}