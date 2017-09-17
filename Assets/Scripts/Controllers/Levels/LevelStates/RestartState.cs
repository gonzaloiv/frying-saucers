using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class RestartState : BaseState {

    #region State Behaviour

    public override void Enter() {
      StartCoroutine(RestartRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator RestartRoutine() {
      yield return new WaitForSeconds(0.8f);
      player.SetActive(true);
      yield return new WaitForSeconds(0.2f);
    }

    #endregion

  }

}