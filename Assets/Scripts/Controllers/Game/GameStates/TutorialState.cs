using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class TutorialState : BaseState {

        #region Fields / Properties

        private const float TUTORIAL_ENDING_TIME = 0.6f;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelScreen.SetActive(true);
            tutorialScreen.SetActive(true);
            tutorialScreen.GetComponent<TutorialScreenController>().Init();
            levelController.gameObject.SetActive(true);
            levelController.ToInitState(GetTutorialLevelData());
        }

        public override void Exit () {
            base.Exit();
            tutorialScreen.SetActive(false);
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            levelController.ToRestartState();
        }

        public void OnWaveEndEvent () {
            StartCoroutine(WaveEndEventRoutine());
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

        #region Private Behaviour

        private IEnumerator WaveEndEventRoutine() {
            yield return StartCoroutine(TimeManager.WaitForRealTime(TUTORIAL_ENDING_TIME));
            gameController.ToMainMenuState();
        }

        #endregion

    }

}