using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class GameOverState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            gameOverScreen.SetActive(true);
            levelController.gameObject.SetActive(false);
        }

        public override void Exit () {
            base.Exit();
            gameOverScreen.SetActive(false);
        }

        public void OnTapInputEvent() {
            gameController.ToLeaderboardState();
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