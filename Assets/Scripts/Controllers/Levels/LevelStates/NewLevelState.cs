using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class NewLevelState : BaseState {

        #region State Behaviour

        public override void Enter () {
            levelScreenController.Initialize();
            levelScreenController.gameObject.SetActive(true);
            waveController.Reset();
            waveController.NewWave(GetCurrentWaveData());
            EventManager.TriggerEvent(new NewLevelEvent());
            player.SetActive(true);
            levelController.ToPlayState();
        }

        #endregion

    }

}