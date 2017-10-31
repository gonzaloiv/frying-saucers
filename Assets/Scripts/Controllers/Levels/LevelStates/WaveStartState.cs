using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveStartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            waveController.Reset();
            waveController.NewWave(GetCurrentLevelData().LevelType, GetCurrentWaveData());
        }

        #endregion

    }

}