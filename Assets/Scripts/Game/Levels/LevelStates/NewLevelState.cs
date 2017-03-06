using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class NewLevelState : BaseState {

    #region State Behaviour

    public override void Enter() {

      if(currentLevelObjects.Count() != 0) {
         currentLevelObjects.ForEach(x =>  x.SetActive(false));
        currentLevelObjects.RemoveAll(x => !x.activeInHierarchy);
      }

      backgroundController.NewLevel();
      hudController.gameObject.SetActive(true);
      hudController.Initialize();

      waveController.Wave(player, currentLevelObjects);

      player.SetActive(true);

    }

    #endregion

  }

}