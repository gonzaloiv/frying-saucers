using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class LevelState : BaseState {

        #region Fields / Properties

        private const float TUTORIAL_LEVEL_INIT_TIME = 0.3f;

        private LevelType levelType;
        private float levelInitTime;

        #endregion

        #region State Behaviour

        public override void Enter() {
            base.Enter();
            levelType = GetCurrentLevelData().LevelType;
            levelInitTime = levelType == LevelType.TutorialLevel ? TUTORIAL_LEVEL_INIT_TIME : 0;
            StartCoroutine(WaveStartRoutine(levelInitTime));
        }

        public void OnEscapeInputEvent () {
            levelController.ToPauseState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            InputManager.EscapeInputEvent += OnEscapeInputEvent;
        }

        protected override void RemoveListeners () {
            InputManager.EscapeInputEvent -= OnEscapeInputEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaveStartRoutine (float levelInitTime) {
            yield return new WaitForSeconds(levelInitTime);
            waveController.InitWave(levelType, GetCurrentWaveData());
        }

        #endregion

    }

}