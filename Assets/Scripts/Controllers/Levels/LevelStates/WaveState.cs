using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveState : BaseState { // TODO: Listening to a WaveEndEvent, to increase de wave number or reduce de current remaining waves.

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
        }

        public override void Exit () {
            base.Exit();
        }

        public void OnEscapeInputEvent () {
            levelController.ToPauseState();
        }

        public void OnWaveEndEvent() {
            levelController.ToWaveStartState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            InputManager.EscapeInputEvent += OnEscapeInputEvent;
            WaveController.WaveEndEvent += OnWaveEndEvent;
        }

        protected override void RemoveListeners () {
            InputManager.EscapeInputEvent -= OnEscapeInputEvent;
            WaveController.WaveEndEvent -= OnWaveEndEvent;
        }

        #endregion

    }

}