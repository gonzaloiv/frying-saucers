using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveStartState : BaseState {

        #region State Behaviour

        public override void Enter () {
            waveController.InitWave(GetCurrentLevelData().LevelType, GetCurrentWaveData());
            waveRefillBehaviour.enabled = GetCurrentLevelData().LevelType != LevelType.TutorialLevel;
        }

        #endregion

    }

}