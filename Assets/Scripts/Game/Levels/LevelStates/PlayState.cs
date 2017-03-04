using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class PlayState : BaseState {

    #region Fields

    private IEnumerator waveRoutine;

    private GameObject currentEnemy;
    private GameObject previousEnemy;

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
      while (currentLevelObjects.Count > 0) {
        yield return new WaitForSeconds(2.1f);
        SetCurrentEnemy();
        if(currentEnemy == previousEnemy)        
          yield return new WaitForSeconds(0.5f);
        currentEnemy.GetComponent<IEnemyBehaviour>().Play();
        previousEnemy = currentEnemy;
      }
    }

    private void SetCurrentEnemy() {
      currentEnemy = previousEnemy;
      if (currentLevelObjects.Count() > 1)
        while (currentEnemy == previousEnemy)
          currentEnemy = currentLevelObjects[Random.Range(0, currentLevelObjects.Count())];
      else
        currentEnemy = currentLevelObjects[0];     
    }

    #endregion

  }


}