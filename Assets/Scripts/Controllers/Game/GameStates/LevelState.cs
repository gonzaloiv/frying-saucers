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

        void OnEscapeInput (EscapeInputEvent escapeInputEvent) {
            gameController.ToPauseState();
        }

        void OnPlayerHitEvent (PlayerHitEvent playerHitEvent) {
            levelController.ToRestartState();
        }

        void OnWaveEndEvent (WaveEndEvent waveEndEvent) {
            levelController.ToNewWaveState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EventManager.StartListening<EscapeInputEvent>(OnEscapeInput);
            EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
            EventManager.StartListening<WaveEndEvent>(OnWaveEndEvent);
        }

        protected override void RemoveListeners () {
            EventManager.StopListening<EscapeInputEvent>(OnEscapeInput);
            EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
            EventManager.StartListening<WaveEndEvent>(OnWaveEndEvent);
        }

        #endregion

    }

}