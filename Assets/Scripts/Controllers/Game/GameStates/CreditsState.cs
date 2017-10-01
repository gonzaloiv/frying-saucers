using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class CreditsState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
			creditsScreen.SetActive(true);
            inputManager.SetActive(false);
            EventManager.TriggerEvent(new CreditsEvent());
        }

        public override void Exit () {
            base.Exit();
			creditsScreen.SetActive(false);
        }

        #endregion

    }

}