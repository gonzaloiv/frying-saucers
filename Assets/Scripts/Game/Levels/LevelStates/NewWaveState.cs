using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace LevelStates {

  public class NewWaveState : BaseState {

    #region State Behaviour
    
    public override void Enter() {
      StartCoroutine(NewWaveRoutine());
    }

    #endregion 

    #region Private Behaviour

    private IEnumerator NewWaveRoutine() {
      yield return new WaitForSeconds(0.5f);
      waveController.Wave(player).ForEach(x => currentLevelObjects.Add(x));
    }

    #endregion
                   	
  }

}