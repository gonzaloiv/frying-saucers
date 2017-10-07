using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LevelState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.InitLevel(GetCurrentLevelData());
            inputManager.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            levelController.ToStopState();
        }

        public void OnEscapeInput (EscapeInputEventArgs escapeInputEventArgs) {
            gameController.ToPauseState();
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            levelController.ToRestartState();
        }

        public void OnWaveEndEvent (WaveEndEventArgs waveEndEventArgs) {
            levelController.ToNewWaveState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            InputManager.EscapeInputEvent += OnEscapeInput;
            PlayerController.PlayerHitEvent += OnPlayerHitEvent;
            WaveController.WaveEndEvent += OnWaveEndEvent;
        }

        protected override void RemoveListeners () {
            InputManager.EscapeInputEvent -= OnEscapeInput;
            PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
            WaveController.WaveEndEvent -= OnWaveEndEvent;
        }

        #endregion

    }

}