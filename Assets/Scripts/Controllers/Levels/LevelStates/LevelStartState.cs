using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class LevelStartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            player = new Player(GetCurrentLevelData().PlayerInitialLives);
            playerController.Init(player);
            playerController.gameObject.SetActive(true);
            levelScreenController.Init(player);
            levelScreenController.gameObject.SetActive(true);
            waveController.Reset();
            waveController.NewWave(GetCurrentLevelData().LevelType, GetCurrentWaveData());
            levelController.ToWaveState();
        }

        #endregion

    }

}