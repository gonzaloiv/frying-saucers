using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

    public class BaseState : State {

        #region Fields

        protected LevelController levelController;
        protected WaveController waveController;
        protected GameObject inputManager;
        protected GameObject player;
        protected LevelScreenController levelScreenController;
        protected GameObject pauseScreen;

        #endregion

        #region Mono Behaviour

        void Awake () {
            levelController = GetComponent<LevelController>();
            waveController = levelController.WaveController;
            inputManager = levelController.InputManagerObject;
            player = levelController.Player;
            levelScreenController = levelController.LevelScreenController;
            pauseScreen = levelController.PauseScreen;
        }

        #endregion

        #region Public Behaviour

        protected WaveData GetCurrentWaveData () {
            return levelController.CurrentWaveData;
        }

        #endregion
  	
    }

}