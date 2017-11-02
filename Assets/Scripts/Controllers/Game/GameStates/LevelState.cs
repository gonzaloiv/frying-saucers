using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LevelState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.ToInitState(GetRandomLevelData());
            levelScreen.SetActive(true);
        }

        public void OnEscapeInputEvent () {
            levelController.ToPauseState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            InputManager.EscapeInputEvent += OnEscapeInputEvent;
        }

        protected override void RemoveListeners () {
            InputManager.EscapeInputEvent -= OnEscapeInputEvent;
        }

        #endregion

    }

}