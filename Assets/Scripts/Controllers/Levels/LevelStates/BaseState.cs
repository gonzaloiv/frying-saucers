using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

    public class BaseState : State {

        #region Fields

        protected LevelData currentLevelData { get { return GetCurrentLevelData(); } }
        protected WaveData currentWaveData { get { return GetCurrentWaveData(); } }

        protected LevelController levelController;
        protected WaveController waveController;
        protected PlayerController playerController;
        protected LevelScreenController levelScreenController;
        protected GameObject pauseScreen;

        protected Player player;

        #endregion

        #region Mono Behaviour

        void Awake () {
            levelController = GetComponent<LevelController>();
            waveController = levelController.WaveController;
            playerController = levelController.PlayerController;
            levelScreenController = levelController.LevelScreenController;
            pauseScreen = levelController.PauseScreen;
            player = new Player();
        }

        #endregion

        #region Public Behaviour

        private LevelData GetCurrentLevelData () {
            return levelController.CurrentLevelData;
        }

        private WaveData GetCurrentWaveData () {
            return levelController.CurrentWaveData;
        }

        #endregion
  	
    }

}