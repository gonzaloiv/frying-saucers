using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

    public class BaseState : State {

        #region Fields

        protected LevelController levelController;
        protected WaveController waveController;
        protected LevelScreenController levelScreenController;
        protected GameObject player;

        #endregion

        #region Mono Behaviour

        void Awake () {
            levelController = GetComponent<LevelController>();
            waveController = levelController.WaveController;
            levelScreenController = levelController.LevelScreenController;
            player = levelController.Player;
        }

        #endregion

        #region Public Behaviour

        protected WaveData GetCurrentWaveData () {
            return levelController.CurrentWaveData;
        }

        #endregion
  	
    }

}