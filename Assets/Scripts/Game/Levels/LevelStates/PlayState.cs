using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class PlayState : BaseState {

    #region Fields

    private IEnumerator waveRoutine;

    #endregion

    #region State Behaviour

    public override void Enter() {
      waveRoutine = WaveRoutine();
      StartCoroutine(waveRoutine);
    }

    public override void Exit() {
      base.Exit();
      StopCoroutine(waveRoutine);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator WaveRoutine() {
      while(currentLevelObjects.Count > 0) {
        yield return new WaitForSeconds(2.5f);
        currentLevelObjects[Random.Range(0, currentLevelObjects.Count())].GetComponent<IEnemyBehaviour>().Play();
      }
    }

    #endregion

  }


}