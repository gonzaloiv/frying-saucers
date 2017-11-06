using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

    public class BaseState : State {

        #region Fields

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
        }

        #endregion

        #region Public Behaviour

        protected LevelData GetCurrentLevelData () {
            return levelController.CurrentLevelData;
        }

        protected WaveData GetCurrentWaveData () {
            return levelController.CurrentWaveData;
        }

        #endregion
  	
    }

}