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

        public void OnPlayerHitEvent () {
            levelController.ToRestartState();
        }

        public void OnWaveEndEvent () {
            levelController.ToNewWaveState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            PlayerController.PlayerHitEvent += OnPlayerHitEvent;
            WaveController.WaveEndEvent += OnWaveEndEvent;
        }

        protected override void RemoveListeners () {
            PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
            WaveController.WaveEndEvent -= OnWaveEndEvent;
        }

        #endregion

    }

}