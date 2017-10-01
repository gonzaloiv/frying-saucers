using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class MainMenuState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            mainMenuScreen.SetActive(true);
            inputManager.SetActive(false);
        }

        public override void Exit () {
            base.Exit();
            mainMenuScreen.SetActive(false);
        }

        #endregion

    }

}