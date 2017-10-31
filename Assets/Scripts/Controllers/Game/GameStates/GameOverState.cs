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
            inputManager.SetActive(false);
        }

        public override void Exit () {
            base.Exit();
            gameOverScreen.SetActive(false);
        }

        #endregion

    }

}