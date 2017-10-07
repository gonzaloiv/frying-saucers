using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class NewLevelState : BaseState {

        #region State Behaviour

        public override void Enter () {
            levelScreenController.gameObject.SetActive(true);
            levelScreenController.Init();
            waveController.Reset();
            waveController.NewWave(GetCurrentWaveData());
            player.SetActive(true);
            levelController.ToPlayState();
        }

        #endregion

    }

}