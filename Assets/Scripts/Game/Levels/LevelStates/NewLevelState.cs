using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

  public class NewLevelState : BaseState {

    #region State Behaviour

    public override void Enter() {
      player = playerSpawner.SpawnPlayer(currentLevelObjects);
      backgroundController.NewLevel();
      hudController.gameObject.SetActive(true);
    }

    #endregion

  }

}