using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LevelState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.InitLevel(GetRandomLevelData());
            inputManager.SetActive(true);
            levelScreen.SetActive(true);
        }

        public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
            if (!playerHitEventArgs.IsDead)
                levelController.ToRestartState();
        }

        public void OnWaveEndEvent () {
            levelController.ToNewWaveState();
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