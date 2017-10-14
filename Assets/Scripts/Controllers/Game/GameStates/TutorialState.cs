using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class TutorialState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.InitLevel(GetTutorialLevelData());
            inputManager.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            levelController.ToStopState();
        }

        #endregion

    }

}