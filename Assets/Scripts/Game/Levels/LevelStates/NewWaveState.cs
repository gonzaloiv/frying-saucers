using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace LevelStates {

  public class NewWaveState : BaseState {

    #region State Behaviour
    
    public override void Enter() {
      waveController.Wave(player).ForEach(x => currentLevelObjects.Add(x));
    }

    #endregion 
      	
  }

}