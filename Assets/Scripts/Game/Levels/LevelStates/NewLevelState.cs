using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class NewLevelState : BaseState {

    #region State Behaviour

    public override void Enter() {


      waveController.Reset();

      backgroundController.NewLevel();
      hudController.gameObject.SetActive(true);
      hudController.Initialize();

      waveController.Wave(player);

      player.SetActive(true);

    }

    #endregion

  }

}