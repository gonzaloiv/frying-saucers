using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class NewLevelState : BaseState {

    #region State Behaviour

    public override void Enter() {

      waveController.Reset();

      hudController.gameObject.SetActive(true);
      hudController.Initialize();

      waveController.NewWave(player, Config.ENEMY_WAVE_AMOUNT);

      player.SetActive(true);

    }

    #endregion

  }

}