using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class LevelStartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            player = new Player(GetCurrentLevelData().PlayerInitialLives);
            levelScreenController.gameObject.SetActive(true);
            levelScreenController.Init(player);
            waveController.Reset();
            waveController.NewWave(GetCurrentWaveData());
            playerController.gameObject.SetActive(true);
            levelController.ToWaveState();
        }

        #endregion

    }

}