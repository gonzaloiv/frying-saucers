using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class TutorialState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.ToInitState(GetTutorialLevelData());
            levelScreen.SetActive(true);
            tutorialScreen.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            tutorialScreen.SetActive(false);
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            levelController.ToRestartState();
        }

        public void OnWaveEndEvent () {
            gameController.ToMainMenuState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
            WaveController.WaveEndEvent += OnWaveEndEvent;
        }

        protected override void RemoveListeners () {
            Player.PlayerHitEvent -= OnPlayerHitEvent;
            WaveController.WaveEndEvent -= OnWaveEndEvent;
        }

        #endregion

    }

}