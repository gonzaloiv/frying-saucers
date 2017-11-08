using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class WaveStartState : BaseState {

        #region Fields / Properties

        private const float TUTORIAL_LEVEL_INIT_TIME = 0.3f;

        private LevelType levelType;
        private float levelInitTime;

        #endregion

        #region State Behaviour

        public override void Enter () {
            base.Enter();
            levelType = currentLevelData.LevelType;
            levelInitTime = levelType == LevelType.TutorialLevel ? TUTORIAL_LEVEL_INIT_TIME : 0;
            StartCoroutine(WaveStartRoutine(levelInitTime));
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveStartRoutine (float levelInitTime) {
            yield return new WaitForSeconds(levelInitTime);
            waveController.InitWave(levelType, currentWaveData);
            levelController.ToWaveState();
        }

        #endregion

    }

}