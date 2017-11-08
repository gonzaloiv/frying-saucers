using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameStates {

    public class LevelState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.ToInitState(GetRandomLevelData());
            levelScreen.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            tutorialScreen.SetActive(false);
        }
            
        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            if (playerHitEventArgs.IsDead) {
                DataManager.AddNewScore(new LeaderboardEntry(playerHitEventArgs.Score, DateTime.Now));
                StartCoroutine(GameOverRoutine());
            }
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
        }

        protected override void RemoveListeners () {
            Player.PlayerHitEvent += OnPlayerHitEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator GameOverRoutine () {
            yield return new WaitForSeconds(0.3f);
            gameController.ToGameOverState();
        }

        #endregion


    }

}