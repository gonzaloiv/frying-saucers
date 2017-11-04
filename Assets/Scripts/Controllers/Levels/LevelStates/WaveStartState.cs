using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveStartState : BaseState {

        #region Fields / Properties

        private const float TUTORIAL_LEVEL_INIT_TIME = 0.3f;
        private LevelType currentLevelType;

        #endregion

        #region State Behaviour

        public override void Enter () {
            base.Enter();
            currentLevelType = GetCurrentLevelData().LevelType;
            float waveStartTime = currentLevelType == LevelType.TutorialLevel ? TUTORIAL_LEVEL_INIT_TIME : 0;
            StartCoroutine(WaveStartRoutine(waveStartTime));
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveStartRoutine (float waveStartTime) {
            yield return new WaitForSeconds(waveStartTime);
            waveController.InitWave(currentLevelType, GetCurrentWaveData());
            waveRefillBehaviour.enabled = GetCurrentLevelData().LevelType != LevelType.TutorialLevel;
            levelController.ToWaveState();
        }

        #endregion

    }

}