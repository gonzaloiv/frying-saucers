using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class NewLevelState : BaseState {

    #region State Behaviour

    public override void Enter() {
      hudController.Initialize();
      hudController.gameObject.SetActive(true);
      waveController.Reset();
      waveController.NewWave(player, currentWave);
      player.SetActive(true);
    }

    #endregion

  }

}